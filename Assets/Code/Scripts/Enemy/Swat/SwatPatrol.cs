using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwatPatrol : AIBaseState<SwatBehaviour>
{
    public SwatPatrol(SwatBehaviour context) : base(context)
    {
    }

    protected override void EnterState()
    {
    }

    protected override void UpdateState()
    {
        NavigationHandler();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {
    }
    protected override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (_context.IsPlayerNear())
        {
            if (_context.IsPlayerOnTarget())
            {
                SwitchStates(_context.currentState,_context.attackState);
                _context.currentState = _context.attackState;
            }
            else
            {
                SwitchStates(_context.currentState, _context.chaseState);
                _context.currentState = _context.chaseState;
            }
        }
    }



    #region Navigation

    private void NavigationHandler()
    {
        if (_context.agent.remainingDistance < .5f)
            PatrolToTheNextPoint();
    }

    private void PatrolToTheNextPoint()
    {
        _context.agent.stoppingDistance = 0f;
        _context.agent.isStopped = false;
        _context._currentPatrolPointIndex = Random.Range(0, _context.patrolPoint.Length);
        _context._currentPatrolPoint = _context.patrolPoint[_context._currentPatrolPointIndex];
        _context.agent.SetDestination(_context._currentPatrolPoint.position);
    }

    #endregion


}
