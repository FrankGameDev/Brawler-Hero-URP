using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }


    PlayerInput _input;

    [Header("Locomotion parameters")]
    public bool IsSpeeding;
    public bool IsJumping;
    public bool StartFlying;

    [Header("Flying parameters")]
    public bool IsFlyingUp;
    public bool IsFlyingDown;

    [Header("Combat parameters")]
    private bool isChargingAttack;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        _input = new PlayerInput();

        // ==== Locomotion
        _input.Locomotion.Movement.canceled += Movement_canceled;

        _input.Locomotion.SpeedUp.performed += OnSpeeding;

        _input.Locomotion.Jump.performed += OnJumping;
        _input.Locomotion.Jump.canceled += OnJumping;

        _input.Locomotion.StartFlying.performed += OnStartFlying;
        _input.Locomotion.StartFlying.canceled += OnStartFlying;

        // ==== Flying
        _input.Flying.FlyUp.performed += FlyUp_performed;
        _input.Flying.FlyUp.canceled += FlyUp_performed;

        _input.Flying.FlyDown.performed += FlyDown_performed;
        _input.Flying.FlyDown.canceled += FlyDown_performed;


        // ==== Combat
        _input.SpecialCombat.ChargedAttack.performed += context => isChargingAttack = true;
        _input.SpecialCombat.ChargedAttack.canceled += context => isChargingAttack = false;
    }


    #region Locomotion

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        IsSpeeding = false;
    }
    void OnSpeeding(InputAction.CallbackContext ctx)
    {

        IsSpeeding ^= ctx.performed;
    }

    void OnJumping(InputAction.CallbackContext ctx)
    {
        IsJumping = ctx.performed;
    }

    void OnStartFlying(InputAction.CallbackContext ctx)
    {
        StartFlying = ctx.ReadValueAsButton();
        IsSpeeding = false;
    }

    public Vector2 GetPlayerMovement() => _input.Locomotion.Movement.ReadValue<Vector2>();

    public bool StopFlying => _input.Locomotion.StopFlying.triggered;


    #endregion


    #region Flying

    private void FlyUp_performed(InputAction.CallbackContext obj)
    {
        IsFlyingUp = obj.performed;
    }
    private void FlyDown_performed(InputAction.CallbackContext obj)
    {
        IsFlyingDown = obj.performed;
    }

    #endregion

    #region Combat


    public bool IsDodging() => _input.Combat.Dodge.WasPressedThisFrame();


    public bool IsAttacking() => _input.Combat.Lightattack.triggered;

    public bool IsGrabbing() => _input.Combat.Grab.triggered;

    public bool IsChargingAttack() => isChargingAttack;


    public bool IsStartedFighting() => IsChargingAttack() || IsAttacking();

    #endregion



    #region Camera

    public Vector2 GetLook() => _input.Camera.LookAround.ReadValue<Vector2>();

    #endregion



    public void EnableAll() => _input.Enable();
    public void DisableAll() => _input.Disable();
    public void EnableLocomotion() => _input.Locomotion.Enable();
    public void DisableLocomotion() => _input.Locomotion.Disable();

    public void EnableCombat() => _input.Combat.Enable();
    public void DisableCombat() => _input.Combat.Disable();

    private void OnEnable()
    {
        _input.Enable();
    }


    private void OnDisable()
    {
        _input.Disable();
    }
}
