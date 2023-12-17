using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Player States/Combat State", fileName = "PlayerCombatSO")]
public class PlayerCombatSOBase : PlayerGroundedSOBase
{
    [Header("Main references")]
    public PlayerCombatStats playerCombatStats;
    PlayerGrabController _playerGrabController;

    [Header("State logic fields"), SerializeField]
    private float fromCombatStateToIdleStateTimer = 1.5f;
    private float maxCombatToIdleTimer;

    [Header("Combat logic parameters")]
    public float enemyFinderRadius;
    public float movementSpeed;

    [Header("Light attack references")]
    public float attackAndGrabHitDetectionRadius;

    [Header("All enemy layers")]
    public LayerMask enemyLayer;

    [Header("Events")]
    public GameEvent grabAndDropEvent;
    public GameEvent throwEvent;
    public GameEventOnPositionSO onAttackHitEvent;

    #region Animation fields

    [Header("Animations")]
    public float fadeToCombatIdleAnimationTime = .5f;
    [Tooltip("Define the attack combo animation. The attack will be executed based on the list order")]
    public List< AttackSO> _attackCombo;

    // == Combo
    int _attackComboIndex = 0;

    #endregion


    // === Utility
    public bool isAttacking { get; private set; }

    Collider _lastEnemyTarget;
    IDamageable _lastEnemyTargetIDamageable;

    #region State machine logic

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        maxCombatToIdleTimer = fromCombatStateToIdleStateTimer;
        player.rb.velocity = Vector3.zero;
        FreeFlowAttack();
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        //Movement();
        if (InputManager.Instance.IsAttacking())
            FreeFlowAttack();

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        _attackComboIndex = 0;
        player.animationController.FadeToAnim(AnimationNames.GROUNDED_LOCOMOTION, .5f); 
    }

    public override void HandleChangeState()
    {
        if ((fromCombatStateToIdleStateTimer -= Time.deltaTime) < 0)
            //|| player.Input.IsSpeeding)
            player.stateMachine.ChangeState(player.playerIdleState);
    }

    public override void ResetValues()
    {
        fromCombatStateToIdleStateTimer = maxCombatToIdleTimer;
    }


    #endregion

    #region Freeflow combat logic

    private void FreeFlowAttack()
    {
        if (isAttacking)
            return;

        ResetValues();
        _lastEnemyTarget = CheckEnemyInRange();
        if (_lastEnemyTarget == null)
        {
            //Se non c'è nessun nemico, esegui solo l'animazione dell'attacco
            AttackComboLogic();
            _lastEnemyTargetIDamageable = null;
            player.lastEnemyTargetIDamageable = _lastEnemyTargetIDamageable;
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
            player.lastEnemyTargetIDamageable = _lastEnemyTargetIDamageable;
        }
    }

    #region Long distance attack

    void HandleLongDistanceAttack()
    {
        ResetAttackState((int)playerCombatStats.attackCooldown);
        MoveTowardsEnemy(_lastEnemyTarget, 1f);
        player.animationController.PlayAnimation(AnimationNames.DROP_KICK);
        player.DisableLocomotionWithTimer(player.disableLocomotionWhenAttackingTimer);
        player.DisableCombatWithTimer(playerCombatStats.attackCooldown, .25f);

        //await GoToCombatIdle();
    }

    void MoveTowardsEnemy(Collider enemyCollider, float movementSpeed = .5f)
    {
        Vector3 enemyPos = TargetOffset(enemyCollider, 2.5f);
        transform.DOMoveZ(enemyPos.z, movementSpeed);
        transform.DOMoveX(enemyPos.x, movementSpeed);
        Vector3 lookAtPos = new(enemyCollider.transform.position.x, player.playerArmature.position.y, enemyCollider.transform.position.z);
        player.playerArmature.DOLookAt(lookAtPos, .5f);
    }

    private Vector3 TargetOffset(Collider target, float offset = 1f)
    {
        Vector3 center = target.bounds.center;
        Vector3 desiredPosition = Vector3.MoveTowards(center, transform.position, .8f);
        Vector3 offsetVector = (desiredPosition - center).normalized * offset;
        return center + offsetVector;
    }


    #endregion

    #region Light attack 

    void HandleLightAttack()
    {
        MoveTowardsEnemy(_lastEnemyTarget);
        player.playerArmature.LookAt(_lastEnemyTarget.transform);
        AttackComboLogic();
    }

    void AttackComboLogic()
    {
        ResetAttackState((int)playerCombatStats.attackCooldown);
        //Disable player inputs
        player.DisableCombatWithTimer(playerCombatStats.attackCooldown, .25f);
        player.DisableLocomotionWithTimer(player.disableLocomotionWhenAttackingTimer);

        player.animationController.PlayAnimation(_attackCombo[_attackComboIndex].AnimationName);
        HandleComboIndex();
        //await GoToCombatIdle();
    }


    void HandleComboIndex()
    {
        if (_attackComboIndex == _attackCombo.Count - 1)
            _attackComboIndex = 0;
        else _attackComboIndex += 1;
    }

    #endregion


    #region Animation utils

    private async Task GoToCombatIdle()
    {
        await player.animationController.WaitForAnimationToFinish();
        await Task.Delay(1 * 1000);
            player.animationController.FadeToAnim(AnimationNames.COMBAT_IDLE, fadeToCombatIdleAnimationTime);
    }


    #endregion



    #region Attack utility


    private Collider CheckEnemyInRange()
    {
        // Check nemico lontano
        if (Physics.SphereCast(transform.position, playerCombatStats.detectLongRangeAttackRadius, player.InputDirection, out RaycastHit hit, playerCombatStats.maxAttackRange, enemyLayer))
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
        Collider[] enemyInSight = Physics.OverlapSphere((player.InputDirection == Vector3.zero) ? transform.position : player.attackPoint.position,
                                                        (player.InputDirection == Vector3.zero) ? enemyFinderRadius : attackAndGrabHitDetectionRadius,
                                                        enemyLayer);
        return enemyInSight.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    AttackType DetectAttackType(Collider enemyCollider)
    {
        if (Vector3.Distance(enemyCollider.transform.position, transform.position) > playerCombatStats.minAttackRange)
            return AttackType.LONG_DISTANCE;

        return AttackType.LIGHT;
    }

    async void ResetAttackState(int attackCD)
    {
        isAttacking = true;
        await Task.Delay(1000 * attackCD);
        isAttacking = false;
    }

    #endregion

    #endregion

    #region Combat movement
    void Movement()
    {
        if (player.movementDisabled)
            return;

        Vector3 movement = player.InputDirection * movementSpeed;
        player.rb.velocity = new Vector3(movement.x, player.rb.velocity.y, movement.z);

        //TODO aggiungere nuova animazione
    }

    #endregion
}

