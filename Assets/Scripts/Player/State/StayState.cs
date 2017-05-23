using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayState : PlayerState {
    public StayState(PlayerController context) : base(context) { _type = Type.stay; }

    public override void ActionDizzy(float time)
    {
        _context.dizzy(time);
    }

    public override void ActionExitResource()
    {

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
        _context.jump();
        _context._state = new JumpState(_context);
    }

    public override void ActionMove(bool left)
    {
        _context.move(left);
    }

    public override void ActionRepel(Vector3 attackerPos, Vector3 repelForce)
    {
        _context.repel(attackerPos, repelForce);
        _context._state = new JumpState(_context);
    }

    public override void ActionShot()
    {
        _context.shot();
    }

    public override void ActionStartPick()
    {
        _context.startPickResource();
        _context._state = new PickingState(_context);
    }

    public override void ActionStopPick()
    {

    }

    public override void ActionTurnTo(bool left)
    {
        _context.turnTo(left);
    }
}
