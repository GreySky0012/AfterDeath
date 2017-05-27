using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// the scenes the player is fireable 
/// </summary>
public class SceneManagerFight : SceneManager
{
	/// <summary>
	/// The gameObjects in the scene.
	/// </summary>
    [HideInInspector]
	public BossController _boss;
    [HideInInspector]
    public GameObject _itemPrefab;

	/// <summary>
	/// The information of this checkpoint
	/// </summary>
	public int _checkPointId;//start from 1(0 means demo)

	/// <summary>
	/// The positions of gameObjects.
	/// </summary>
	public Vector3 _bossPostion;
    public Dictionary<Vector3, CommonData.ResourceType> _resourceList = new Dictionary<Vector3, CommonData.ResourceType>();//the resrouces in this scene
    [HideInInspector]
    public Transform _itemPos;//the position of the item getting text
    [HideInInspector]
    public Transform _buffPos;//the position of the buff icon
    [HideInInspector]
    public float _buffSize;//the size of the buff icon

    public void Awake()
    {
        _ui = Camera.Instantiate(PrefabManager.Instance._scene_ui);
        InitScene();
    }

    public void Start()
    {
        _buffSize = 45f;
        _itemPrefab = PrefabManager.Instance._text_getitem;
    }

    override protected void InitScene()
    {
        base.InitScene();
        InstantiateBoss();
    }

	void InstantiateBoss()
	{
		_boss = Camera.Instantiate (PrefabManager._instance._BossPrefab [_checkPointId]).GetComponent<BossController> ();
	}

    /// <summary>
    /// show the win scene
    /// </summary>
    public void Win()
    {
    }

    /// <summary>
    /// show the lose scene
    /// </summary>
    public void Lose()
    {

    }

    /// <summary>
    /// instantiate the player and set some reference
    /// </summary>
    override protected void InstantiateHero()
    {
        _player = GameManager.Instance._player.InstantiateHero(true, _playerPosition);
        _player._hpSlider = _ui.transform.Find("HPBar").Find("Slider").GetComponent<Slider>();
        _player._hpSlider.maxValue = _player._maxHealth;
        _player._hpText = _ui.transform.Find("HPBar").Find("HPText").GetComponent<Text>();
        _player._weapon._overHeatSlider = _ui.transform.Find("WeaponBar").Find("Slider").GetComponent<Slider>();
        _player._weapon._overHeatSlider.maxValue = _player._weapon._overHeatMax;
        _itemPos = _ui.transform.Find("TextPosition");
        _buffPos = _ui.transform.Find("BuffPosition");
    }

    /// <summary>
    /// get some resources
    /// </summary>
    /// <param name="id">resource id</param>
    /// <param name="num">resource number</param>
    public void GetItem(int id, int num)
    {
        string content = CommonData.Instance._resList[(CommonData.ResourceType)id]._name + "+" + num.ToString();

        GameObject go = Instantiate(_itemPrefab, _itemPos.position, Quaternion.identity) as GameObject;
        go.GetComponent<GetItemText>().UpdateText(content);
    }

    /// <summary>
    /// reset the buff's position
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="index">the index of this buff in the buff manager</param>
    public void SetBuffPosition(AutoReduce buff,int index)
    {
        buff.transform.position = _buffPos.transform.position + new Vector3(index * _buffSize, 0f, 0f);
    }
}