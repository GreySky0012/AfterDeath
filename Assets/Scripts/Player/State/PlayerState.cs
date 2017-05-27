using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the state of the player,deal all actions of the player
/// the different state can behave differently in front of different actions
/// </summary>
public abstract class PlayerState{

    public enum Type { stay, jump, picking };

    public Type _type;
    public PlayerController _context;

    public PlayerState(PlayerController context)
    {
        _context = context;
    }

    public abstract void ActionTurnTo(bool left);
    public abstract void ActionMove(bool left);
    public abstract void ActionJump();
    public abstract void ActionShot();
    public abstract void ActionStartPick();
    public abstract void ActionStopPick();
    public abstract void ActionExitResource();
    public abstract void ActionHurt(float damage, float invicibleTime);
    public abstract void ActionRepel(Vector3 attackerPos, Vector3 repelForce);
    public abstract void ActionImprisoned(float time);
    public abstract void ActionDizzy(float time);
}