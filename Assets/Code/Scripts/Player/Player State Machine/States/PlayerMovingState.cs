using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerState
{
    public PlayerMovingState(PlayerController player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerMovingInstance.DoEnterLogic();
    }

    public override void Update()
    {
        base.Update();
        player.playerMovingInstance.DoUpdateLogic();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.playerMovingInstance.DoFixedUpdateLogic();
    }
}
