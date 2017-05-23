using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// the class of a player
/// </summary>
public class Player{

    public PlayerInfo _info;
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

	public Player()
    {
        _info = Saver.Read();
	}

	public PlayerController InstantiateHero(bool fireable,Vector3 position)
	{
		PlayerController player = PlayerFactory.CreatePlayer(_info,fireable);

        InitHero(player,position);

		return player;
	}

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