using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private static CameraHandler _instance;

    public static CameraHandler Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Free look camera fields")]
    public CinemachineFreeLook groundedCamera;
    public CinemachineFreeLook flyingCamera;

    [Header("Situational camera")]
    public CinemachineVirtualCamera combatCamera;
    public CinemachineTargetGroup targetGroup;

    // Active objects
    private CinemachineFreeLook activeCamera;

    private Transform secondActiveTargetObject = null;

    private float groundedCamFoV, flyingCamFoV;

    float _velocity;


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        groundedCamFoV = groundedCamera.m_Lens.FieldOfView;
        flyingCamFoV = flyingCamera.m_Lens.FieldOfView;
    }

    #region Free look

    public void EnableGroundedCamera()
    {
        EnableCamera(groundedCamera.gameObject);
        activeCamera = groundedCamera;
    }

    public void EnableFlyingCamera()
    {
        EnableCamera(flyingCamera.gameObject);
        activeCamera = flyingCamera;
    }


    public void SetCameraFoV(float fov)
    {
        activeCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(activeCamera.m_Lens.FieldOfView, fov, ref _velocity, 1f);
    }


    public void ResetCameraFov(CameraType cameraType)
    {
        CinemachineFreeLook cameraToReset = null;
        float resetFoV = 40f;

        switch (cameraType)
        {
            case CameraType.GROUND:
                cameraToReset = groundedCamera;
                resetFoV = groundedCamFoV;
                break;
            case CameraType.FLY:
                cameraToReset = flyingCamera;
                resetFoV = flyingCamFoV;
                break;
        }

        cameraToReset.m_Lens.FieldOfView = Mathf.SmoothDamp(cameraToReset.m_Lens.FieldOfView, resetFoV, ref _velocity, .6f);

    }

    #endregion


    #region Combat camera


    public void EnableCombatCamera(Transform secondTarget)
    {
        EnableCamera(combatCamera.gameObject);
        if (secondTarget != null)
            AddSecondTarget(secondTarget);
        else
            throw new MissingReferenceException("Target per combat camera assente");
    }

    private void DisableCombatCamera()
    {
        combatCamera.gameObject.SetActive(false);

        if (secondActiveTargetObject == null)
            return;

        targetGroup.RemoveMember(secondActiveTargetObject);
        secondActiveTargetObject = null;
    }

    public void AddSecondTarget(Transform secondTarget)
    {
        if (secondActiveTargetObject != null)
        {
            targetGroup.RemoveMember(secondActiveTargetObject);
            secondActiveTargetObject = null;
        }

        targetGroup.AddMember(secondTarget, .3f, 0);
        secondActiveTargetObject = secondTarget;
    }

    #endregion


    private void EnableCamera(GameObject selectedCamera)
    {
        DisableAllCameras();
        selectedCamera.SetActive(true);
    }

    private void DisableAllCameras()
    {
        groundedCamera.gameObject.SetActive(false);
        flyingCamera.gameObject.SetActive(false);
        DisableCombatCamera();
    }
}

public enum CameraType
{
    GROUND,
    FLY,
    LOCK_ON
}
