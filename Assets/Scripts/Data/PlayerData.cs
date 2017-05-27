using UnityEngine;
using System.Collections;

/// <summary>
/// the origin data of a player
/// </summary>
public class PlayerData
{
    private static PlayerData _instance;
    public static PlayerData Instance
    { get
        {
            if (_instance == null)
                _instance = new PlayerData();
            return _instance;
        }
    }

    public float _defense_origin { get; private set; }
    public float _moveSpeed_origin { get; private set; }
    public float _jumpForce_origin { get; private set; }
    public float _repulseRate_origin { get; private set; }
	public float _collectionTime_origin{ get; private set; }
    public float _invicibleTimeRate_origin { get; private set; }

    private PlayerData()
    {
		SetData ();
    }

    /// <summary>
    /// Init the origin data
    /// </summary>
	public void SetData()
	{
		_defense_origin = 1f;
		_moveSpeed_origin = 0.12f;
		_jumpForce_origin = 750f;
        _repulseRate_origin = 1f;
		_collectionTime_origin = 2f;
        _invicibleTimeRate_origin = 1f;
    }
}