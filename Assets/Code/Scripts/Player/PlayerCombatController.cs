using System.Linq;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(InputManager))]
public class PlayerCombatController : MonoBehaviour
{

    public Transform playerArmature;

    [Header("Main references")]
    public PlayerCombatStats playerCombatStats;
    Rigidbody _rb;
    private Animator _animator;
    PlayerGrabController _playerGrabController;

    [Header("Combat logic parameters")]
    public float enemyFinderRadius;

    [Header("Light attack references")]
    public Transform attackPoint;
    public float attackAndGrabHitDetectionRadius;

    [Header("All enemy layers")]
    public LayerMask enemyLayer;

    [Header("Events")]
    public GameEvent grabAndDropEvent;
    public GameEvent throwEvent;
    public GameEventOnPositionSO onAttackHitEvent;

    #region Animation fields
    private PlayerAnimationController animationController { get => PlayerAnimationController.Instance; }
   
    [Header("Animations")]
    public int fadeToCombatIdleTime;

    #endregion

    // == Combo
    int _attackComboIndex = 0;
    Dictionary<int, string> _attackComboAnimationOrder;

    // === Utility
    public bool isAttacking { get; private set; }
    Vector3 _inputDir
    {
        get
        {
            Vector3 movementInput = InputManager.Instance.GetPlayerMovement();
            return Camera.main.transform.right * movementInput.x + Camera.main.transform.forward * movementInput.y;
        }
    }
    Collider _lastEnemyTarget;
    IDamageable _lastEnemyTargetIDamageable;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        TryGetComponent(out _playerGrabController);
    }

    private void Start()
    {
        _attackComboAnimationOrder = new Dictionary<int, string>
        {
            { 0, AnimationNames.HOOK_PUNCH_RX },
            { 1, AnimationNames.MARTELO_KICK },
            { 2, AnimationNames.HEAD_BUTT },
            { 3, AnimationNames.HOOK_PUNCH_LX },
            { 4, AnimationNames.KICK }
        };
    }

    private void Update()
    {
        if (InputManager.Instance.IsAttacking())
        {
            if (_playerGrabController.IsEnemyGrabbed())
                throwEvent?.RaiseEvents();
            else
                FreeFlowAttack();
        }

        if (InputManager.Instance.IsGrabbing())
            grabAndDropEvent?.RaiseEvents();

    }

    #region Freeflow combat

    private void FreeFlowAttack()
    {
        if (isAttacking)
            return;

        _lastEnemyTarget = CheckEnemyInRange();
        if (_lastEnemyTarget == null)
        {
            //Se non c'è nessun nemico, esegui solo l'animazione dell'attacco
            AttackComboLogic();
            _lastEnemyTargetIDamageable = null;
            return;
        }

        AttackType attackType = DetectAttackType(_lastEnemyTarget);

        if (_lastEnemyTarget.TryGetComponent(out _lastEnemyTargetIDamageable)) //Estraggo l'Idamageable
        {
            isAttacking = true;
            switch (attackType)
            {
                case AttackType.LONG_DISTANCE:
                    HandleLongDistanceAttack();
                    break;
                case AttackType.LIGHT:
                    HandleLightAttack();
                    break;
            }
            
        }
    }

    //Called by animation key point
    public void OnEnemyHit()
    {
        if (_lastEnemyTarget == null)
            return;

        onAttackHitEvent?.RaiseEvent(_lastEnemyTarget.bounds.center);
        _lastEnemyTargetIDamageable.TakeDamage(playerCombatStats.lightAttackDmg);
    }


    #region Long distance attack

    void HandleLongDistanceAttack()
    {
        MoveTowardsEnemy(_lastEnemyTarget, 1f);
        //TODO evento
        animationController.PlayAnimation(AnimationNames.DROP_KICK);
        float animTime = animationController.GetAnimationLength();
        StartCoroutine(DisableLocomotionWithTimer(animTime));
        StartCoroutine(DisableCombatWithTimer(animTime));
    }

    void MoveTowardsEnemy(Collider enemyCollider, float movementSpeed = .5f)
    {
        Vector3 enemyPos = TargetOffset(enemyCollider);
        transform.DOMoveZ(enemyPos.z, movementSpeed);
        transform.DOMoveX(enemyPos.x, movementSpeed);
        playerArmature.LookAt(enemyCollider.transform);
    }

    private Vector3 TargetOffset(Collider target)
    {
        Vector3 center = target.bounds.center;
        return Vector3.MoveTowards(center, transform.position, .8f);
    }


    #endregion

    #region Light attack 

    void HandleLightAttack()
    {
        MoveTowardsEnemy(_lastEnemyTarget);
        playerArmature.LookAt(_lastEnemyTarget.transform);
        AttackComboLogic();
    }

    void AttackComboLogic()
    {
        StartCoroutine(DisableCombatWithTimer(playerCombatStats.attackCooldown));
        StartCoroutine(DisableLocomotionWithTimer(playerCombatStats.attackCooldown));

        //TODO risolvere errore primo attacco
        animationController.PlayAnimation(_attackComboAnimationOrder[_attackComboIndex]);

        HandleComboIndex();
    }


    void HandleComboIndex()
    {
        if (_attackComboIndex == _attackComboAnimationOrder.Count - 1)
            _attackComboIndex = 0;
        else _attackComboIndex += 1;
    }

    #endregion

    private Collider CheckEnemyInRange()
    {
        // Check nemico lontano
        if (Physics.SphereCast(transform.position, playerCombatStats.detectLongRangeAttackRadius, _inputDir, out RaycastHit hit, playerCombatStats.maxAttackRange, enemyLayer))
            return hit.collider;

        // Check nemico vicino, se non è presente nessun nemico lontano
        return NearEnemyFinder();
    }

    /**
     * Se il giocatore non sta muovendo il personaggio, verrà scelto il nemico più vicino, indipendentemente dalla direzione del personaggio.
     * Altrimenti, viene trovato il nemico più vicino in base alla direzione scelta
     */
    private Collider NearEnemyFinder()
    {
        Collider[] enemyInSight = Physics.OverlapSphere((_inputDir == Vector3.zero) ? transform.position : attackPoint.position,
                                                        (_inputDir == Vector3.zero) ? enemyFinderRadius : attackAndGrabHitDetectionRadius,
                                                        enemyLayer);
        return enemyInSight.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    #region Attack utility

    IEnumerator DisableLocomotionWithTimer(float seconds = 1f)
    {
        InputManager.Instance.DisableLocomotion();
        yield return new WaitForSeconds(seconds);
        InputManager.Instance.EnableLocomotion();
    }

    IEnumerator DisableCombatWithTimer(float seconds)
    {
        isAttacking = true;
        InputManager.Instance.DisableCombat();

        yield return new WaitForSeconds(seconds);

        InputManager.Instance.EnableCombat();
        //TODO: Wait affinchè l'animazione non termina
        animationController.FadeToAnim(AnimationNames.COMBAT_IDLE, fadeToCombatIdleTime);
        isAttacking = false;
    }


    AttackType DetectAttackType(Collider enemyCollider)
    {
        if (Vector3.Distance(enemyCollider.transform.position, transform.position) > playerCombatStats.minAttackRange)
            return AttackType.LONG_DISTANCE;

        return AttackType.LIGHT;
    }

    #endregion

    #endregion


    //#region Combat animation

    //public void PlayAnimation(int animationHash)
    //{
    //    if (animationHash == _currentAnimation)
    //        return;

    //    _animator.Play(animationHash);
    //    _currentAnimation = animationHash;
    //}

    //private void CrossFadeAnimToCombatIdle()
    //{
    //    if (isAttacking) return; 
    //    _animator.CrossFade(CombatIdleHash, fadeToIdleTime);
    //}


    //#endregion


    public Collider GetLastEnemyTarget() => _lastEnemyTarget;


    public void ResetLastEnemyTarget(bool b = false)
    {
        if (_lastEnemyTarget == null) return;

        if (Vector3.Distance(_lastEnemyTarget.transform.position, playerArmature.position) > attackAndGrabHitDetectionRadius)
            _lastEnemyTarget = null;

        if (b)
            _lastEnemyTarget = null;
    }


    private void OnDrawGizmos()
    {
        //Freeflow and grab
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position, attackAndGrabHitDetectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyFinderRadius);

        //Long distance attack gizmos
        //Gizmos.color = Color.black;
        //Gizmos.DrawSphere(playerArmature.forward * playerCombatStats.maxAttackRange, playerCombatStats.detectLongRangeAttackRadius);
    }
}

//enum AttackType
//{
//    LONG_DISTANCE,
//    LIGHT
//}

//enum LightAttackType
//{
//    KICK, PUNCH, HEAD
//}