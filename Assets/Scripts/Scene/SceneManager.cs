using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    #region The gameObjects in the scene.
    [HideInInspector]
    public PlayerController _player;
    public Vector3 _playerPosition = new Vector3(-7, -4, -1);
    [HideInInspector]
    public GameObject _ui;//the ui of the scene
    #endregion

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

    /// <summary>
    /// add an instantiated object into the UI of the scene
    /// </summary>
    /// <param name="obj"></param>
    public void AddObjectToUI(GameObject obj)
    {
        obj.transform.SetParent(_ui.transform);
    }
}
