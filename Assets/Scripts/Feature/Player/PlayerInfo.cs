using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the info of a player to store in the local file
/// </summary>
public class PlayerInfo{

    public Bag _bag;

	public PlayerInfo()
    {
        readData();
    }

    public void resetCollectionData(int[] collections)
    {
        _bag = new Bag(collections);
    }

    public void readData()
    {
        //read the collection data from the local file
        int[] collections = new int[CommonData.Instance.resourceNum];
        resetCollectionData(collections);
    }
}
