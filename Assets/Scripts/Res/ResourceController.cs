using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public CommonData.ResourceType _type;
    public ResourceInfo _info;

    int _associatedType = -1;//the associated type of this resource(-1 means none)
    
	int _collectionNum;//The collected value

    /// <summary>
    /// Init the resources
    /// </summary>
    /// <param name="type"></param>
    public void Init(CommonData.ResourceType type)
    {
        _info = CommonData.Instance._resList[_type];
        _associatedType = -1;
    }

    /// <summary>
    /// Init the resource(if associated with some other resources)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="associatedRate">the rate of associated,max = 100</param>
    public void Init(CommonData.ResourceType type,CommonData.ResourceType associatedType, int associatedRate)
    {
        _info = CommonData.Instance._resList[_type];
        _associatedType = -1;
        int random = (int)Random.value * 100;
        if (random <= associatedRate)
        {
            _associatedType = (int)associatedType;
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
    /// 注意:如果资源转换成了伴生资源的类型，那么这个脚本的资源类型就会改变，如果调用函数没写好可能导致错误地获得了伴生资源，如果有必要的话可以设置一个标志位在下一帧的Update函数里改变资源类型
	/// </summary>
	public int Collect(PlayerController picker)
	{
        int collectionNum = Random.Range(_info._minReward, _info._maxReward);

        //if the resources are endless
        if (_info._maxValue == -1)
            return collectionNum;

        if (_info._maxValue - _collectionNum - collectionNum <= _info._middleValue)
        {

            if (_associatedType != -1)
            {
                collectionNum = _info._maxValue - _info._middleValue - _collectionNum;
                ChangeToAssociated();
                return collectionNum;
            }
            else
            {
                ChangeImage();
            }
        }

        if (collectionNum > _info._maxValue - _collectionNum)
        {
            collectionNum = _info._maxValue - _collectionNum;
            Get(picker);
        }

		return collectionNum;
	}

    /// <summary>
    /// change the image to the second
    /// </summary>
    private void ChangeImage()
    {
        GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance._backImages[(int)_type];
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