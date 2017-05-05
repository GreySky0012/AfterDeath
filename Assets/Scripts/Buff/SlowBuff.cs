using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBuff : Buff {

    public SlowBuff(float existTime):base(existTime)
    {
        _buffType = BuffType.slow;
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
        playerData._moveSpeed = playerData_origin._moveSpeed / 2;
    }

    public override void CalBullet(PlayerBullet bullet) { }
}
