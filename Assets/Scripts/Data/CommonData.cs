using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonData
{
	public enum ResourceType { JinShuCanHai, WaSi, ShiYou, KuangShi, LieYanShi, MingJieHua, HuXiaoShi };
    public int resourceNum = 7;//the num of resource types
	public Dictionary<ResourceType, ResourceInfo> _resList= new Dictionary<ResourceType, ResourceInfo>();

	private static CommonData _instance;
	public static CommonData Instance
	{ get
		{
			if (_instance == null) {
				_instance = new CommonData ();
				_instance.Init ();
			}
			return _instance;
		}
	}

	void Init(){
		LoadResourceInfo ();
	}

	/// <summary>
	/// 加载所有资源信息
	/// </summary>
	void LoadResourceInfo()
	{
        /*ResourceInfo jinshucanhai = new ResourceInfo("金属残骸", "no", 5, 10 , 100 , ResourceManager.Instance._images[0]);
        ResourceInfo shiyou = new ResourceInfo("石油", "no", 5, 10, -1, ResourceManager.Instance._images[1]);
        ResourceInfo wasi = new ResourceInfo("瓦斯", "no", 5, 10, -1, ResourceManager.Instance._images[2]);
        ResourceInfo kuangshi = new ResourceInfo("矿石", "no", 3, 4, 30, ResourceManager.Instance._images[3]);
        ResourceInfo lieyanshi = new ResourceInfo("烈焰石", "no", 1, 2, 5, ResourceManager.Instance._images[4]);
        ResourceInfo mingjiehua = new ResourceInfo("冥界花", "no", 1, 2, 5, ResourceManager.Instance._images[5]);
        ResourceInfo huxiaoshi = new ResourceInfo("呼啸石", "no", 1, 2, 5, ResourceManager.Instance._images[6]);

		_resList.Add(ResourceType.JinShuCanHai, jinshucanhai);
		_resList.Add(ResourceType.ShiYou, shiyou);
		_resList.Add(ResourceType.WaSi, wasi);
		_resList.Add(ResourceType.KuangShi, kuangshi);
		_resList.Add(ResourceType.LieYanShi, lieyanshi);
		_resList.Add(ResourceType.MingJieHua, mingjiehua);
		_resList.Add(ResourceType.HuXiaoShi, huxiaoshi);*/
	}
}