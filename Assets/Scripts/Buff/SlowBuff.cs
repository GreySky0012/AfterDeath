using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the specific slow buff
/// </summary>
public class SlowBuff : Buff {

    public SlowBuff(float existTime):base(existTime)
    {
        _buffType = BuffType.slow;
        _iconSprite = Resources.Load("Pictures/Player/icon_slow", typeof(Sprite)) as Sprite;
    }

    public override bool CheckMove()
    {
        return true;
    }

    public override bool CheckControl()
    {
        return true;
    }

    public override void CalPlayerData(PlayerController.Data playerData_origin, PlayerController.Data playerData)
    {
        playerData._moveSpeed = playerData._moveSpeed / 2;
    }

    public override void CalWeapon(Weapon weapon) { }

    public override void BuffUpdate(PlayerController.Data player) { }
}
