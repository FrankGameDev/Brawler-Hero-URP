using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SwatChase : AIBaseState<SwatBehaviour>
{

    private float _destinationTimer = .25f;
    private float _timer;

    public SwatChase(SwatBehaviour context) : base(context)
    {
    }


    protected override void EnterState()
    {
        _context.agent.enabled = true;
    }

    protected override void ExitState()
    {
    }

    protected override void UpdateState()
    {
        FollowPlayer();
        CheckSwitchStates();
    }

    protected override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (_context.IsPlayerOnTarget())
        {
            SwitchStates(_context.currentState, _context.attackState);
            _context.currentState = _context.attackState;
        }
        else if (!_context.IsPlayerNear())
        {
            SwitchStates(_context.currentState, _context.patrolState);
            _context.currentState = _context.patrolState;
        }
    }

    void FollowPlayer()
    {
        if(_timer <= _destinationTimer)
        {
            _timer += Time.deltaTime;
            return;
        }
        _context.agent.SetDestination(_context.playerTransform.position);
        _timer = 0;
    }


}
