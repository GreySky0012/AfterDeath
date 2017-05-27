using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag{
    
    public int[] _collections;//the resources already owned

    public Bag()
    {
        _collections = new int[CommonData.Instance.resourceNum];
    }

    public Bag(int[] collection)
    {
        Init(collection);
    }

    public Bag(Bag bag):this(bag._collections) { }

    public void Init(int[] collection)
    {
        _collections = (int[])collection.Clone();
    }

    /// <summary>
    /// get some resources
    /// </summary>
    /// <param name="type"></param>
    /// <param name="num"></param>
    public void AddResource(CommonData.ResourceType type,int num)
    {
        _collections[(int)type] += num;
    }
}
