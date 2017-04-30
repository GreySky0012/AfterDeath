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

	public Player(){
        _info = new PlayerInfo();
	}

	public PlayerController InstantiateHero(Vector3 position)
	{
		GameObject hero = Camera.Instantiate (PrefabManager.Instance._hero);

        InitHero(hero,position);

		return hero.GetComponent<PlayerController> ();
	}

    /// <summary>
    /// init the hero object
    /// </summary>
    private void InitHero(GameObject hero,Vector3 position)
    {
        hero.transform.position = position;

        PlayerController controller = hero.GetComponent<PlayerController>();

        controller._bag = new Bag(_info._bag);
    }
}