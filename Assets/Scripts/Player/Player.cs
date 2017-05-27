using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// the player of the game system(not for the scene)
/// </summary>
public class Player{

    public PlayerInfo _info;//the info keeping in line with the local file 
    private static Player _instance = null;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }

    /// <summary>
    /// read inform from the local file when the player instantiate
    /// </summary>
	public Player()
    {
        _info = Saver.Read();
	}

    /// <summary>
    /// instantiate the player into the scene
    /// </summary>
    /// <param name="fireable">is the scene fireable</param>
    /// <param name="position"></param>
    /// <returns></returns>
	public PlayerController InstantiateHero(bool fireable,Vector3 position)
	{
		PlayerController player = PlayerFactory.CreatePlayer(_info,fireable);

        InitHero(player,position);

		return player;
	}

    /// <summary>
    /// when the info of the player change reset the info and save it into the local file
    /// </summary>
    /// <param name="info"></param>
    public void ResetInfo(PlayerInfo info)
    {
        _info.Init(info);
        GameManager.Instance.Save();
    }

    /// <summary>
    /// init the hero object
    /// </summary>
    private void InitHero(PlayerController player,Vector3 position)
    {
        player.transform.position = position;
        player._info = new PlayerInfo(_info);
    }
}