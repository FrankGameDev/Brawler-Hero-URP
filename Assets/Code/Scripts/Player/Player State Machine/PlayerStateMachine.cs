using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState { get; set; }

    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void ChangeState(PlayerState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
}
