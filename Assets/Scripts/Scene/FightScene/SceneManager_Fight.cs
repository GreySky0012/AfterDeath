using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

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
	/// The infomation of this checkpoint
	/// </summary>
	public int _checkPointId;//start from 1
	public float _animTime = 0;//The time of the animation before fighting

	/// <summary>
	/// The positions of gameObjects.
	/// </summary>
	public Vector3 _bossPostion;
    public Dictionary<Vector3, CommonData.ResourceType> _resourceList = new Dictionary<Vector3, CommonData.ResourceType>();
    [HideInInspector]
    public Transform _itemPos;//the position of the item get text
    [HideInInspector]
    public Transform _buffPos;//the position of the buff icon
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

	protected void AwakeFight()
	{
		_player._controllable = true;
		_boss.Active ();
	}

	void InstantiateBoss()
	{
		_boss = Camera.Instantiate (PrefabManager._instance._BossPrefab [_checkPointId]).GetComponent<BossController> ();
	}

    /// <summary>
    /// show the endding of this scene
    /// </summary>
    public void BossDeath()
    {
    }

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
    /// 获得物品
    /// </summary>
    /// <param name="id">物品id</param>
    /// <param name="num">物品数量</param>
    public void GetItem(int id, int num)
    {
        string content = CommonData.Instance._resList[(CommonData.ResourceType)id]._name + "+" + num.ToString();

        GameObject go = Instantiate(_itemPrefab, _itemPos.position, Quaternion.identity) as GameObject;
        go.GetComponent<GetItemText>().UpdateText(content);
    }

    public void SetBuffPosition(AutoReduce buff,int index)
    {
        buff.transform.position = _buffPos.transform.position + new Vector3(index * _buffSize, 0f, 0f);
    }
}