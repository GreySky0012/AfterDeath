using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController context) : base(context) { _type = Type.jump; }

    public override void ActionDizzy(float time)
    {
        _context.dizzy(time);
    }

    public override void ActionExitResource()
    {
        _context.exitResource();
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
        _context.move(left);
    }

    public override void ActionRepel(Vector3 attackerPos, Vector3 repelForce)
    {
        _context.repel(attackerPos, repelForce);
    }

    public override void ActionShot()
    {
        _context.shot();
    }

    public override void ActionStartPick()
    {

    }

    public override void ActionStopPick()
    {

    }

    public override void ActionTurnTo(bool left)
    {
        _context.turnTo(left);
    }
}
