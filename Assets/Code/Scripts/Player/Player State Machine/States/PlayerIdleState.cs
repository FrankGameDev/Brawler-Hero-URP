using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerGroundedInstance.DoEnterLogic();
    }

    public override void Update()
    {
        base.Update();
        player.playerGroundedInstance.DoUpdateLogic();
    }
}
