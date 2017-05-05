using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
