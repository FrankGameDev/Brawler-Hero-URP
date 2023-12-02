using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerController player;
    protected PlayerStateMachine playerStateMachine;

    public PlayerState(PlayerController player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    /// <summary>
    /// Se si esegue l'override, non rimuovere il base.Update(), altrimenti non viene eseguito il controllo sul cambio di stati
    /// </summary>
    public virtual void Update() { HandleChangeState(); }
    public virtual void FixedUpdate() { }


    public virtual void HandleChangeState() { } 
}
