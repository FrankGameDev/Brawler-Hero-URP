using UnityEngine;


public class PlayerCombatState : PlayerState
{
    public PlayerCombatState(PlayerController player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerCombatInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        player.playerCombatInstance.DoExitLogic();

    }

    public override void Update()
    {
        base.Update();
        player.playerCombatInstance.DoUpdateLogic();
    }
}
