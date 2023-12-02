using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatAttack : AIBaseState<SwatBehaviour>
{

    public SwatAttack(SwatBehaviour context) : base(context)
    {
    }



    protected override void EnterState()
    {
        _context.agent.enabled = false;
        _context.AimingAnimation(true);
    }

    // Update is called once per frame
    protected override void UpdateState()
    {
        _context.Shot();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {
        _context.shotTime = _context.shotCooldown;
        _context.agent.enabled = true;
        _context.AimingAnimation(false);
    }



    protected override void FixedUpdateState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (!_context.IsPlayerOnTarget())
        {
            if (_context.IsPlayerNear())
            {
                SwitchStates(_context.currentState, _context.chaseState);
                _context.currentState = _context.chaseState;
            }
            else
            {
                SwitchStates(_context.currentState, _context.patrolState);
                _context.currentState = _context.patrolState;
            }
        }
    }
}
