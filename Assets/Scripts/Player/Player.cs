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
        _info = new PlayerInfo();
	}

	public PlayerController InstantiateHero(bool fireable,Vector3 position)
	{
		PlayerController player = PlayerFactory.CreatePlayer(_info,fireable);

        InitHero(player,position);

		return player;
	}

    /// <summary>
    /// init the hero object
    /// </summary>
    private void InitHero(PlayerController player,Vector3 position)
    {
        player.transform.position = position;

        player._bag = new Bag(_info._bag);
    }
}