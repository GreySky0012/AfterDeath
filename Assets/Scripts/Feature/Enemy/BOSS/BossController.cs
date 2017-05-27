using System;
using System.Collections;
using UnityEngine;


public class BossController:Enemy
{
    public override void Death()
    {
        ((SceneManagerFight)GameManager.Instance._scene).Win();
        base.Death();
    }
}