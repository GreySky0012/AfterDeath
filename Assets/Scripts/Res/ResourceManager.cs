using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour{

    static private ResourceManager _instance;
    static public ResourceManager Instance { private set { } get { return _instance; } }

    public void Awake()
    {
        _instance = this;
    }

    public Sprite[] _beforeImages;//the first image of the resources
    public Sprite[] _backImages;//the second image of the resources(if no,the same with first)

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