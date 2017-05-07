using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Checker{

    List<AutoReduce> _buffs = new List<AutoReduce>();

    public BuffManager() { }

    public void CreateBuff(Buff.BuffType type,float existTime)
    {
        foreach (AutoReduce buff in _buffs)
        {
            if (buff._buff._buffType == type)
            {
                existTime = buff._restTime > existTime ? buff._restTime : existTime;
                RemoveBuff(buff);
                break;
            }
        }

        switch (type)
        {
            case Buff.BuffType.slow:
                InstantiateBuff(new SlowBuff(existTime));
                break;
        }
    }

    public void RemoveBuff(AutoReduce buff)
    {
        if(_buffs.Contains(buff))
            _buffs.Remove(buff);
    }

    void InstantiateBuff(Buff buff)
    {
        AutoReduce b = Camera.Instantiate(PrefabManager.Instance._buffTemplet).GetComponent<AutoReduce>();
        GameManager.Instance._scene.AddObjectToUI(b.gameObject);
        b.Init(buff);
        _buffs.Add(b);
    }

    /// <summary>
    /// reset the buff icons into the right position
    /// </summary>
    public void CheckBuffs()
    {
        for(int i = 0;i<_buffs.Count;i++)
        {
            ((SceneManagerFight)GameManager.Instance._scene).SetBuffPosition(_buffs[i], i);
        }
    }

    #region traversal the buffs to play a part
    /// <summary>
    /// check is the player moveable
    /// </summary>
    /// <returns></returns>
    public bool CheckMove()
    {
        foreach (AutoReduce b in _buffs)
        {
            if (!b._buff.CheckMove())
                return false;
        }
        return true;
    }

    /// <summary>
    /// check is the player controllable
    /// </summary>
    /// <returns></returns>
    public bool CheckControl()
    {
        foreach (AutoReduce b in _buffs)
        {
            if (!b._buff.CheckControl())
                return false;
        }
        return true;
    }

    /// <summary>
    /// Process the data of the player
    /// </summary>
    /// <param name="playerData"></param>
    public void CalPlayerData(PlayerController.Data playerData_origin, PlayerController.Data player)
    {
        foreach (AutoReduce b in _buffs)
        {
            b._buff.CalPlayerData(playerData_origin, player);
        }
    }

    /// <summary>
    /// Process a bullet
    /// </summary>
    /// <param name="bullet"></param>
    public void CalBullet(PlayerBullet bullet)
    {
        foreach (AutoReduce b in _buffs)
        {
            b._buff.CalBullet(bullet);
        }
    }
    #endregion
}