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
    [HideInInspector]
    public GameObject _ui;

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
        _player = GameManager.Instance._player.InstantiateHero(false,_playerPosition);
    }

    public void AddObjectToUI(GameObject obj)
    {
        obj.transform.SetParent(_ui.transform);
    }
}
