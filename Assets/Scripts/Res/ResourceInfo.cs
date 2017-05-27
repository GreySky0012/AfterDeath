using UnityEngine;
using System.Collections;

public class ResourceInfo
{
    public string _name { get; private set; }
    public string _description { get; private set; }
    public int _maxReward;
    public int _minReward;
    public int _maxValue;//the max collected value
    public int _middleValue;//the middle collected value(-1 means unlimited)(change image to the second or change the resource type to the associated)

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="maxReward"></param>
    /// <param name="minReward"></param>
    /// <param name="maxValue"></param>
    /// <param name="middleValue">-1 means never change</param>
	public ResourceInfo(string name, string description,int maxReward,int minReward,int maxValue,int middleValue)
    {
        _name = name;
        _description = description;
        _maxReward = maxReward;
        _minReward = minReward;
        _maxValue = maxValue;
        _middleValue = middleValue;
    }
}
