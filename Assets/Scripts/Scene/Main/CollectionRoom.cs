using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionRoom : MonoBehaviour {

    public GameObject[] collections;
    public Material grey_material;
    Material normal;

	// Use this for initialization
	void Start ()
    {
        reset();
	}

    void reset()
    {/*
        int[] resources = Player.Instance._info._bag.GetCollections();
        normal = collections[0].GetComponent<Renderer>().material;
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i] == 0)
                collections[i].GetComponent<Renderer>().material = grey_material;
            else
                collections[i].GetComponent<Renderer>().material = normal;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
