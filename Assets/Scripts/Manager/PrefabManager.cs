using UnityEngine;
using System.Collections;

/// <summary>
/// hold references of prefabs
/// </summary>
public class PrefabManager : MonoBehaviour {

	public static PrefabManager _instance;
    public static PrefabManager Instance { private set { } get { return _instance; } }

	public GameObject[] _BossPrefab;
	public GameObject _hero;
    public GameObject[] _resources;
    public GameObject _scene_ui;
    public GameObject[] _weapons;
    public GameObject _text_getitem;
    public GameObject _buffTemplet;

    private void Awake()
    {
        _instance = this;
    }
}
