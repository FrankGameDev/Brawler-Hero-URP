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
        _input.Gameplay.Movement.canceled += Movement_canceled;

        _input.Gameplay.SpeedUp.performed += OnSpeeding;

        _input.Gameplay.Jump.performed += OnJumping;
        _input.Gameplay.Jump.canceled += OnJumping;

        _input.Gameplay.StartFlying.performed += OnStartFlying;
        _input.Gameplay.StartFlying.canceled += OnStartFlying;

        // ==== Flying
        _input.Flying.FlyUp.performed += FlyUp_performed;
        _input.Flying.FlyUp.canceled += FlyUp_performed;

        _input.Flying.FlyDown.performed += FlyDown_performed;
        _input.Flying.FlyDown.canceled += FlyDown_performed;


        // ==== Combat
        _input.Gameplay.ChargedAttack.performed += context => isChargingAttack = true;
        _input.Gameplay.ChargedAttack.canceled += context => isChargingAttack = false;
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

    public Vector2 GetPlayerMovement() => _input.Gameplay.Movement.ReadValue<Vector2>();

    public bool StopFlying => _input.Gameplay.StopFlying.triggered;


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


    public bool IsDodging() => _input.Gameplay.Dodge.WasPressedThisFrame();


    public bool IsAttacking() => _input.Gameplay.Lightattack.triggered;

    public bool IsGrabbing() => _input.Gameplay.Grab.triggered;

    public bool IsChargingAttack() => isChargingAttack;


    public bool IsStartedFighting() => IsChargingAttack() || IsAttacking();

    #endregion



    #region Camera

    public Vector2 GetLook() => _input.Gameplay.LookAround.ReadValue<Vector2>();

    #endregion



    private void EnableAll() => _input.Enable();
    private void DisableAll() => _input.Disable();
    public void EnableGameplayInputs() => _input.Gameplay.Enable();
    public void DisableGameplayInputs() => _input.Gameplay.Disable();

    private void OnEnable()
    {
        _input.Enable();
    }


    private void OnDisable()
    {
        _input.Disable();
    }
}
