using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Asset bundl.
/// </summary>



public class MyWeb_Game2 : MonoBehaviour
{
    void Start()
    {
	   InitializationSence();
    }

	public List<AssetBundleItem> bundleInfoList=new List<AssetBundleItem>();
	/// <summary>
	/// in this funcion,finished the initialization sence
	/// </summary>
	private void InitializationSence(){
		LoadAssetBundle.Instance.getAllAssets ("web",bundleInfoList,"bundle.unity3d",CallBack_getAssetsList);
	}

	/// <summary>
	/// load the initialization prefab;
	/// </summary>
	private void CallBack_getAssetsList()
	{
		for (int i = 0; i < bundleInfoList.Count; i++)
		{
			AssetBundleItem _item=bundleInfoList[i];
			if (_item.immediately)
			{
				LoadAssetBundle.Instance.LoadGameObject (_item,null);
				//Debug.Log(_item.Name);
			}
		}
	}





}
