

using UnityEngine;

public abstract class AIBaseState<StateMachine>
{
    // Reference to main objects
    protected StateMachine _context;

    public AIBaseState(StateMachine context)
    {
        _context = context;
    }


    public void EnterStates()
    {
        EnterState();
    }

    protected abstract void EnterState();


    /**
     * Esegui le azioni definite per lo stato corrente.
     */
    public void UpdateStates()
    {
        UpdateState();
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
    }

    protected abstract void UpdateState();

    protected abstract void FixedUpdateState();


    /**
     * Esci dallo stato corrente.
     */
    public void ExitStates()
    {
        ExitState();
    }

    protected abstract void ExitState();

  
    protected void SwitchStates(AIBaseState<StateMachine> currentState, AIBaseState<StateMachine> newState)
    {
        //TODO aggiungere current state e i vari exit
        currentState.ExitStates();
        newState.EnterStates();
    }


    public abstract void CheckSwitchStates();
}
