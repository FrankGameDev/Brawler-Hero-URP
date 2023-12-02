using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerVFX : MonoBehaviour
{
    [Header("Attack VFX section"), Space(1)]

    [Header("Attack VFX parameters")]
    public GameObject AttackHitEffect;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    public GameEventOnPositionSO onAttackHitEvent;


    [Space, Header("Dash Trail section"), Space(1)]

    [Header("Dodge animation constant")]
    public float maxTrailTime = 2f;
    public float meshRefreshRate = .1f;
    public float meshDestroyTime = 3f;

    [Header("Shader references and parameters")]
    public Material trailMaterial;
    public string trailAlphaRef;
    public float shaderVarRate = .1f;
    public float shaderVarRefreshRate = .05f;

    [Header("Player references")]
    public Transform playerCollider;
    public Transform playerArmatureTransform;

    private bool _isTrailActive;

    private ObjectPool<GameObject> trailMeshes;

    private void OnEnable()
    {
        onAttackHitEvent.onEventRequested += OnAttackHitEffect;
    }

    private void OnDisable()
    {
        onAttackHitEvent.onEventRequested -= OnAttackHitEffect;
    }

    private void Start()
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        trailMeshes = new ObjectPool<GameObject>(CreateSkinnedMeshRenderer, OnGetSkinnedMesh, OnReleaseSkinnedMesh, OnDestroySkinnedMesh, maxSize: 10);
    }

    #region Dash trail

    private GameObject CreateSkinnedMeshRenderer()
    {
        GameObject gObj = new GameObject();

        MeshRenderer meshRenderer = gObj.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gObj.AddComponent<MeshFilter>();
        return gObj;
    }

    void OnGetSkinnedMesh(GameObject obj)
    {
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(playerCollider.position, playerArmatureTransform.rotation);

        Mesh mesh = new Mesh();
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        _skinnedMeshRenderer.BakeMesh(mesh, meshFilter);

        meshFilter.mesh = mesh;
        meshRenderer.material = trailMaterial;
        StartCoroutine(AnimateMaterialFloat(meshRenderer.material, 0, shaderVarRate, shaderVarRefreshRate, obj));
    }

    void OnReleaseSkinnedMesh(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroySkinnedMesh(GameObject obj)
    {
        Destroy(obj);
    }

    public void ActiveTrail()
    {
        if (_isTrailActive) return;

        StartCoroutine(ActivateTrail());
    }


    IEnumerator ActivateTrail()
    {
        float timer = 0;
        _isTrailActive = true;
        yield return new WaitWhile(() =>
        {
            timer += meshRefreshRate;

            trailMeshes.Get();
            return timer <= maxTrailTime;
        });

        _isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate, GameObject obj)
    {
        float valueToAnim = mat.GetFloat(trailAlphaRef);
        while (valueToAnim > goal)
        {
            valueToAnim -= rate;
            mat.SetFloat(trailAlphaRef, valueToAnim);
            yield return new WaitForSeconds(refreshRate);
        }
        trailMeshes.Release(obj);
    }

#endregion


    public void OnAttackHitEffect(Vector3 attackPosition)
    {
        GameObject obj = Instantiate(AttackHitEffect, attackPosition, Quaternion.identity);
        Destroy(obj, 1f);
    }
}

