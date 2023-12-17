using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{

    #region State machine variables

    public PlayerStateMachine stateMachine;

    public PlayerIdleState playerIdleState;
    public PlayerMovingState playerMovingState;
    public PlayerJumpingState playerJumpingState;
    public PlayerCombatState playerCombatState;

    #endregion

    #region State machine Scriptable objects

    /*
     * Instance of the state machine scriptable object exists because i want to be able to change the state of the player at runtime, without 
     * changing scriptable object properties and serialize it.
     */

    [SerializeField] private PlayerIdleSOBase playerGroundedSO;
    [SerializeField] private PlayerMovingSOBase playerMovingSO;
    [SerializeField] private PlayerJumpingSOBase playerJumpingSO;
    [SerializeField] private PlayerCombatSOBase playerCombatSO;

    [HideInInspector] public PlayerIdleSOBase playerGroundedInstance;
    [HideInInspector] public PlayerMovingSOBase playerMovingInstance;
    [HideInInspector] public PlayerJumpingSOBase playerJumpingInstance;
    [HideInInspector] public PlayerCombatSOBase playerCombatInstance;
    #endregion

    [Header("References")]
    public PlayerAnimationController animationController;
    public InputManager Input => InputManager.Instance;
    public CameraHandler CameraHandler => CameraHandler.Instance;

    [Header("Player attributes")]
    public PlayerStats playerStats;
    public PlayerCombatStats playerCombatStats;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Transform cameraTransform;
    [HideInInspector] public int currentHealth;

    [Header("Player transform parameters")]
    public Transform playerArmature;
    public Transform orientation;
    public float rotationSpeed;

    [Header("Grounded fields")]
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundLayerMask;

    [Header("Combat fields")]
    public Transform attackPoint;
    public float disableLocomotionWhenAttackingTimer;

    [Header("Events")]
    public GameEvent dashEvent;

    // ---- Player locomotion
    [HideInInspector] public bool movementDisabled;
    public bool canDodge = true;
    public Vector3 InputDirection
    {
        get
        {
            Vector2 movementInput = Input.GetPlayerMovement();
            Vector3 inputDirection = (cameraTransform.right * movementInput.x + cameraTransform.forward * movementInput.y).normalized;
            return inputDirection;
        }
    }

    public MMF_Player Feedback;


    private void Awake()
    {
        //Reference init
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        currentHealth = playerStats.hp;

        //State init
        playerIdleState = new PlayerIdleState(this, stateMachine);
        playerMovingState = new PlayerMovingState(this, stateMachine);
        playerJumpingState = new PlayerJumpingState(this, stateMachine);
        playerCombatState = new PlayerCombatState(this, stateMachine);

        //State SO instantiation
        playerGroundedInstance = Instantiate(playerGroundedSO);
        playerMovingInstance = Instantiate(playerMovingSO);
        playerJumpingInstance = Instantiate(playerJumpingSO);
        playerCombatInstance = Instantiate(playerCombatSO);

        stateMachine = new PlayerStateMachine();

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGroundedInstance.Initialize(gameObject, this);
        playerMovingInstance.Initialize(gameObject, this);
        playerJumpingInstance.Initialize(gameObject, this);
        playerCombatInstance.Initialize(gameObject, this);

        stateMachine.Initialize(playerIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        RotatePlayer();
        Dodge();

        Debug.Log(stateMachine.currentState);
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public void RotatePlayer()
    {
        Vector3 viewDir = transform.position - new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        orientation.forward = viewDir.normalized;

        Vector2 movementInput = Input.GetPlayerMovement();
        Vector3 inputDir = orientation.right * movementInput.x + orientation.forward * movementInput.y;
        Quaternion targetRotation = Quaternion.LookRotation(inputDir != Vector3.zero
                                                                ? inputDir
                                                                : playerArmature.transform.forward);

        if (InputDirection != Vector3.zero)
            playerArmature.transform.rotation = Quaternion.Slerp(playerArmature.transform.rotation,
                                                                 targetRotation,
                                                                 Time.deltaTime * rotationSpeed);
    }

    #region Dodge

    public void Dodge()
    {
        if (!canDodge)
            return;

        if (!InputManager.Instance.IsDodging())
            return;

        Vector3 dodgeDirection = InputDirection == Vector3.zero ? Camera.main.transform.forward : InputDirection;
        rb.AddForce(playerStats.dodgeSpeed * dodgeDirection, ForceMode.Impulse);
        dashEvent?.RaiseEvents();
        StartCoroutine(DisableMovementForDodging(.25f));
        StartCoroutine(ResetDodge());
    }

    IEnumerator DisableMovementForDodging(float sec = .5f)
    {
        movementDisabled = true;
        yield return new WaitForSeconds(sec);
        movementDisabled = false;
    }

    IEnumerator ResetDodge()
    {
        canDodge = false;
        yield return new WaitForSeconds(playerStats.dodgeCooldown);
        canDodge = true;
    }

    #endregion

    #region Combat

    public void DisableCombatWithTimer(float seconds, float fadeToCombatIdleTime) => StartCoroutine(DisableCombatWithTimerCoroutine(seconds, fadeToCombatIdleTime));

    public void DisableLocomotionWithTimer(float seconds = 1) => StartCoroutine(DisableLocomotionWithTimerCoroutine(seconds));

    private IEnumerator DisableLocomotionWithTimerCoroutine(float seconds = 1f)
    {
        InputManager.Instance.DisableGameplayInputs();
        yield return new WaitForSeconds(seconds);
        InputManager.Instance.EnableGameplayInputs();
    }

    private IEnumerator DisableCombatWithTimerCoroutine(float seconds, float fadeToCombatIdleTime)
    {
        InputManager.Instance.DisableGameplayInputs();

        yield return new WaitForSeconds(seconds);

        InputManager.Instance.EnableGameplayInputs();
        //TODO: Wait affinchè l'animazione non termina
        yield return new WaitUntil(() => !animationController.AnimatorIsPlaying());
    }

    #endregion

    #region Utility 

    public bool IsGrounded() => Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundLayerMask);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);

        ////Freeflow and grab
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(attackPoint.position, playerCombatInstance.attackAndGrabHitDetectionRadius);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, playerCombatInstance.enemyFinderRadius);

        ////Long distance attack gizmos
        //Gizmos.color = Color.black;
        //Gizmos.DrawSphere(playerArmature.forward * playerCombatStats.maxAttackRange, playerCombatStats.detectLongRangeAttackRadius);
    }

    #endregion

    #region Animation keypoint functions

    public GameEventOnPositionSO onAttackHitEvent;
    public IDamageable lastEnemyTargetIDamageable;

    //Called by animation key point
    public void OnEnemyHit()
    {
        if (lastEnemyTargetIDamageable == null)
            return;

        onAttackHitEvent?.RaiseEvent(attackPoint.position);
        lastEnemyTargetIDamageable.TakeDamage(playerCombatStats.lightAttackDmg);
    }
    #endregion

    #region IDamageable

    public void TakeDamage(int damage)
    {
        Debug.Log("Aia");
    }

    #endregion
}
