using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the info of a player to store in the local file
/// </summary>
public class PlayerInfo{

    public Bag _bag;
    public Weapon.Type _weaponType = Weapon.Type.gun;
    public string[] _techs = {"none","none","doubleJump"};
    public bool[] _techOwn = new bool[1]{false};

    public PlayerInfo()
    {
        _bag = new Bag();
    }

    public PlayerInfo(PlayerInfo info)
    {
        Init(info);
    }

    public void Init(PlayerInfo info)
    {
        _bag = new Bag(info._bag);
        _techs = (string[])info._techs.Clone();
        _techOwn = (bool[])info._techOwn.Clone();
    }

    public void resetCollectionData(int[] collections)
    {
        _bag = new Bag(collections);
    }

    public void resetTech(string[] techs)
    {
        _techs = techs;
    }

    public void resetTechOwn(bool[] techOwn)
    {
        _techOwn = techOwn;
    }
}