//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerFlyState : OldPlayerBaseState
//{
//    // == Flying parameters
//    private float _maxFlySpeed;
//    private Vector3 _flyingDirection;

//    // == Enter state parameter
//    private float _timeOffsetAfterSwitch;

//    private CameraType _cameraType;

//    // == Utility
//    Vector3 _smoothDampVelocity;


//    public PlayerFlyState(OldPlayerStateMachine context, OldPlayerStateFactory factory) : base(context, factory)
//    {
//        _isRootState = true;
//        InitialSubState();

//        _maxFlySpeed = _context.playerStats.maxFlySpeed;
//        _cameraType = CameraType.FLY;
//    }

//    #region State machine 

//    protected override void EnterState()
//    {
//        _context.rb.useGravity = false;

//        _timeOffsetAfterSwitch = 0f;

//        PlayerAnimationController.Instance.SetFlying(true);

//        CameraHandler.Instance.EnableFlyingCamera();

//    }

//    protected override void UpdateState()
//    {
//        CheckSwitchStates();

//        GetFlyingDirection();
//        RotatePlayerInAir();

//        if (_context.inputManager.IsSpeeding)
//        {
//            CameraHandler.Instance.SetCameraFoV(80f);
//        }
//        else
//        {
//            CameraHandler.Instance.ResetCameraFov(_cameraType);
//        }
//    }

//    protected override void FixedUpdateState()
//    {
//        Fly();
//        VerticalFly();
//    }

//    protected override void ExitState()
//    {
//        CameraHandler.Instance.ResetCameraFov(_cameraType);
//        _context.rb.useGravity = true;
//        PlayerAnimationController.Instance.SetFlying(false);
//    }

//    public override void CheckSwitchStates()
//    {
//        if (_timeOffsetAfterSwitch < 1f)
//        {
//            _timeOffsetAfterSwitch += Time.deltaTime;
//            return;
//        }

//        if (_context.inputManager.StopFlying || _context.GetGroundDistance() < 1)
//            SwitchStates(_factory.GetPlayerGroundState());
//    }

//    public override void InitialSubState()
//    {
//    }

//    #endregion


//    void VerticalFly()
//    {
//        float flyingSpeed = _context.playerStats.normalFlySpeed;

//        if (_context.inputManager.IsFlyingUp)
//        {
//            _context.rb.velocity = new Vector3(_context.rb.velocity.x, flyingSpeed, _context.rb.velocity.z);
//        }
//        else if (_context.inputManager.IsFlyingDown)
//        {
//            _context.rb.velocity = new Vector3(_context.rb.velocity.x, -flyingSpeed, _context.rb.velocity.z);
//        }

//    }


//    void Fly()
//    {
//        if (_context.movementDisabled)
//            return;

//        float flyingSpeed = !_context.inputManager.IsSpeeding ? _context.playerStats.normalFlySpeed : _context.playerStats.maxFlySpeed;

//        if (_flyingDirection != Vector3.zero)
//        {
//            float timeToMaxSpeed = _context.playerStats.timeToMaxSpeed;
//            if (_context.inputManager.IsSpeeding)
//                timeToMaxSpeed /= 1.5f;

//            _context.rb.velocity = Vector3.SmoothDamp(_context.rb.velocity, OverclockOrLimitVelocity(flyingSpeed), ref _smoothDampVelocity, timeToMaxSpeed);
//        }
//        else
//        {
//            _context.rb.velocity = Vector3.SmoothDamp(_context.rb.velocity, Vector3.zero, ref _smoothDampVelocity, .2f);
//        }

//        // Animation and other setting related
//        PlayerAnimationController.Instance.SetVelocity(_context.rb.velocity.magnitude);
//    }


//    Vector3 OverclockOrLimitVelocity(float speed)
//    {
//        float flyingSpeed = speed;

//        if (_flyingDirection.y <= -0.5f)
//        {
//            flyingSpeed = _context.playerStats.maxFlyDiveSpeed;
//        }

//        return Vector3.ClampMagnitude(flyingSpeed * _flyingDirection, flyingSpeed);
//    }

//    void RotatePlayerInAir()
//    {
//        Vector3 viewDir = _context.transform.position - new Vector3(_context.cameraTransform.position.x, _context.cameraTransform.transform.position.y, _context.cameraTransform.position.z);
//        _context.orientation.forward = viewDir.normalized;

//        Vector2 movementInput = _context.inputManager.GetPlayerMovement();
//        Vector3 inputDir = _context.orientation.right * movementInput.x + _context.orientation.forward * movementInput.y;
//        //TODO fix, inserire rotazione con player in verticale
//        if (_flyingDirection != Vector3.zero)
//            _context.playerObj.forward = Vector3.Slerp(_context.playerObj.forward, inputDir.normalized, Time.deltaTime * _context.rotationSpeed);
//    }

//    void GetFlyingDirection()
//    {
//        _flyingDirection = _context.inputDirection;
//    }


//}
