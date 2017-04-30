using System;
using System.Collections;
using UnityEngine;


public class BossController:Enemy
{
	/// <summary>
	/// Active this boss
	/// </summary>
	public void Active()
	{

	}

    public override void Death()
    {
        ((SceneManagerFight)GameManager.Instance._scene).BossDeath();
        base.Death();
    }
}