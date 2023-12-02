using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    public PlayerJumpingState(PlayerController player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerJumpingInstance.DoEnterLogic();
    }

    public override void Update()
    {
        base.Update();
        player.playerJumpingInstance.DoUpdateLogic();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.playerJumpingInstance.DoFixedUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.playerJumpingInstance.DoExitLogic();
    }
}
