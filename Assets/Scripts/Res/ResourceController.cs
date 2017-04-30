using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public CommonData.ResourceType _type;
    public ResourceInfo _info;

    int _associatedType = -1;//the associated type of this resource
    int _associatedValue;//when the associated resource be showed
    
	int _collectionNum;//The value the resource has been collected

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// set the associated of the resource
    /// </summary>
    /// <param name="type"></param>
    /// <param name="associatedRate">the rate of associated,max = 100</param>
    public void Associated(CommonData.ResourceType type,int associatedRate,int associatedValue)
    {
        _info = CommonData.Instance._resList[_type];
        _associatedType = (int)type;
        int random = (int)Random.value * 100;
        if (random <= associatedRate)
        {
            _associatedType = (int)type;
            _associatedValue = associatedValue;
        }
    }

	/// <summary>
	/// This resource is got.
	/// </summary>
	void Get(PlayerController picker){
        picker.ExitResource();
		Destroy (gameObject);
	}
		
	/// <summary>
	/// Collect this instance.
    /// note:if the resource change to the associated the type of this resource will change to the associated type!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	/// </summary>
	/// <returns><c>true</c>,this resource has been collected</returns>
	public int Collect(PlayerController picker)
	{
        int collectionNum = Random.Range(_info._minReward, _info._maxReward);

        if (_info._maxNum == -1)
            return collectionNum;

        if (collectionNum > _info._maxNum - _collectionNum)
            collectionNum = _info._maxNum - _collectionNum;

        _collectionNum += collectionNum;

        if (_associatedType != -1)
        {
            if (_collectionNum >= _associatedValue)
            {
                ChangeToAssociated();
            }
        }

        if (_collectionNum >= _info._maxNum) {
			Get (picker);
		}

		return collectionNum;
	}

    /// <summary>
    /// change the resource to the associated type
    /// </summary>
    private void ChangeToAssociated()
    {
        if (_associatedType == -1)
            return;
        _type = (CommonData.ResourceType)_associatedType;
        _info = CommonData.Instance._resList[_type];
        _collectionNum = 0;
        _associatedType = -1;
    }
}
