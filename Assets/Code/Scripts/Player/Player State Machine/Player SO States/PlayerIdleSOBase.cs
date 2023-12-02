using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player States/Idle State", fileName = "PlayerIdleSO")]
public class PlayerIdleSOBase : PlayerGroundedSOBase
{

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.rb.velocity = Vector3.zero;
        player.animationController.PlayAnimation(AnimationNames.GROUNDED_LOCOMOTION);
        player.animationController.SetGroundedLocomotionVelocity(0);

        player.CameraHandler.EnableGroundedCamera();
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
    }

    public override void HandleChangeState()
    {

        if (player.Input.IsAttacking())
        {
            player.stateMachine.ChangeState(player.playerCombatState);
        }
        else if (player.InputDirection != Vector3.zero)
            player.stateMachine.ChangeState(player.playerMovingState);
        else if (player.Input.IsJumping)
            player.stateMachine.ChangeState(player.playerJumpingState);
    }
}
