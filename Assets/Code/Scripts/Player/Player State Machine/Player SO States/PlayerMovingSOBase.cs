using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player States/Moving State", fileName = "PlayerMovingSO")]
public class PlayerMovingSOBase : PlayerGroundedSOBase
{

    public override void DoEnterLogic()
    {
        player.animationController.PlayAnimation(AnimationNames.GROUNDED_LOCOMOTION);
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
    }
    public override void DoFixedUpdateLogic()
    {
        base.DoFixedUpdateLogic();
        Movement();
    }

    public override void HandleChangeState()
    {
        if (player.Input.IsAttacking())
        {
            player.stateMachine.ChangeState(player.playerCombatState);
        }
        else if (player.InputDirection == Vector3.zero)
        {
            player.stateMachine.ChangeState(player.playerIdleState);
        }
        else if (player.Input.IsJumping)
            player.stateMachine.ChangeState(player.playerJumpingState);
    }

    void Movement()
    {
        if (player.movementDisabled)
            return;

        float playerSpeed = player.Input.IsSpeeding ? player.playerStats.runningSpeed : player.playerStats.walkingSpeed;

        Vector3 movement = player.InputDirection * playerSpeed;
        player.rb.velocity = new Vector3(movement.x, player.rb.velocity.y, movement.z);

        player.animationController.SetGroundedLocomotionVelocity(playerSpeed);
    }


}
