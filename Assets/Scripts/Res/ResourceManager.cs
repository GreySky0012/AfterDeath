using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    static private ResourceManager _instance;
    static public ResourceManager Instance { private set { } get { return _instance; } }

    public Sprite[] _images;

    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ResourceController InstantiateResource(CommonData.ResourceType type, Vector3 position)
    {
        GameObject resource = Camera.Instantiate(PrefabManager._instance._resources[(int)type]);

        InitResource(resource, position);

        return resource.GetComponent<ResourceController>();
    }

    private void InitResource(GameObject resource, Vector3 position)
    {
        resource.transform.position = position;
    }
}