using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the manager of the player's buffs(under the player controller)
/// </summary>
public class BuffManager {

    List<AutoReduce> _buffs = new List<AutoReduce>();

    public BuffManager() { }

    /// <summary>
    /// produce a specific buff
    /// </summary>
    /// <param name="type"></param>
    /// <param name="existTime"></param>
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

    /// <summary>
    /// remove a specific buff
    /// </summary>
    /// <param name="buff">the buff to remove</param>
    public void RemoveBuff(AutoReduce buff)
    {
        if(_buffs.Contains(buff))
            _buffs.Remove(buff);
    }

    /// <summary>
    /// remove all buffs
    /// </summary>
    public void RemoveAll()
    {
        foreach (AutoReduce b in _buffs)
            _buffs.Remove(b);
    }

    /// <summary>
    /// instantiate a buff icon
    /// </summary>
    /// <param name="buff"></param>
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

    #region traversal the buffs to play a roll
    /// <summary>
    /// is the player moveable
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
    /// is the player controllable
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
    /// cal the player data with the buffs' influences
    /// </summary>
    /// <param name="playerData"></param>
    public void CalPlayerData(PlayerController.Data playerData_origin, PlayerController.Data player)
    {
        player.Set(playerData_origin);
        foreach (AutoReduce b in _buffs)
        {
            b._buff.CalPlayerData(playerData_origin, player);
        }
    }

    /// <summary>
    /// cal the weapon data with the buffs' influences
    /// </summary>
    /// <param name="bullet"></param>
    public void CalWeapon(Weapon weapon)
    {
        weapon.Init();
        foreach (AutoReduce b in _buffs)
        {
            b._buff.CalWeapon(weapon);
        }
    }

    /// <summary>
    /// called in the player's update function
    /// </summary>
    /// <param name="player"></param>
    public void BuffsUpdate(PlayerController.Data player)
    {
        foreach (AutoReduce b in _buffs)
        {
            b._buff.BuffUpdate(player);
        }
    }
    #endregion
}