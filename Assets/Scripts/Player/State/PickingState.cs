using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingState : PlayerState
{
    public PickingState(PlayerController context) : base(context) { }

    public override void ActionDizzy(float time)
    {
        _context.stopPickResource();
        _context.dizzy(time);
        _context._state = new StayState(_context);
    }

    public override void ActionExitResource()
    {
        _context.stopPickResource();
        _context._state = new StayState(_context);
    }

    public override void ActionHurt(float damage, float invicibleTime)
    {
        _context.hurt(damage, invicibleTime);
    }

    public override void ActionImprisoned(float time)
    {
        _context.imprisoned(time);
    }

    public override void ActionJump()
    {

    }

    public override void ActionMove(bool left)
    {

    }

    public override void ActionRepel(Vector3 attackerPos, Vector3 repelForce)
    {
        _context.stopPickResource();
        _context._state = new JumpState(_context);
    }

    public override void ActionShot()
    {

    }

    public override void ActionStartPick()
    {

    }

    public override void ActionStopPick()
    {
        _context.stopPickResource();
        _context._state = new StayState(_context);
    }

    public override void ActionTurnTo(bool left)
    {

    }
}
