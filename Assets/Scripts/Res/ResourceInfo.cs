using UnityEngine;
using System.Collections;

public class ResourceInfo
{
    public string _name { get; private set; }
    public string _description { get; private set; }
    public int _maxReward;
    public int _minReward;
    public int _maxNum;
    public Sprite _image;

	public ResourceInfo(string name, string description,int maxReward,int minReward,int maxNum,Sprite image)
    {
        _name = name;
        _description = description;
        _maxReward = maxReward;
        _minReward = minReward;
        _maxNum = maxNum;
        _image = image;
    }
}
