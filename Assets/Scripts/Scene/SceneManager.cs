using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    /// <summary>
    /// The gameObjects in the scene.
    /// </summary>
    [HideInInspector]
    public PlayerController _player;
    public Vector3 _playerPosition = new Vector3(-7, -4, -1);

    public void Awake()
    {
        InitScene();
    }

    virtual protected void InitScene()
    {
        InstantiateHero();
    }

    virtual protected void InstantiateHero()
    {
        _player = GameManager.Instance._player.InstantiateHero(_playerPosition);
        _player.InstantiateWeapon(Weapon.WeaponType.gun).transform.parent = _player.transform.Find("hand");
    }
}
