using UnityEngine;
using System.Collections;
using System.Collections.Generic;









public class AssetBundleController : MonoBehaviour
{

	#region  sigleton
	public string GAME_NAME="game2";
	private static AssetBundleController _instance;
	public static AssetBundleController Instance
	{
		get
	    { 
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType (typeof(AssetBundleController)) as AssetBundleController;
			}
			return _instance;
		}
	}
	public List<AssetBundleItem> bundleInfoList=new List<AssetBundleItem>();
	void Start()
	{
		LoadAssetBundle.Instance.getAllAssets(GAME_NAME,bundleInfoList,"bundle.unity3d",CallBack_getAssetsList);
	}
	public List<AssetBundleItem> myBundleInfoList=new List<AssetBundleItem>();
	private void CallBack_getAssetsList()
	{
		for (int i = 0; i < bundleInfoList.Count; i++)
		{
			AssetBundleItem _item=bundleInfoList[i];
		    Debug.Log (_item.Path);
		}
		if (bundleInfoList.Count > 0) {
			isMayLoading = true;
		}
	}


	public bool isLoading=false;
	public bool isMayLoading=false;
	void Update()
	{
		if (isMayLoading&&!isLoading)
		{
			int k = 0;
			for (int i = 0; i < bundleInfoList.Count; i++)
			{
				if (!string.IsNullOrEmpty (bundleInfoList [i].Name)) 
				{
					if (bundleInfoList [i].isLoaded == false) 
					{
						isLoading = true;
						getLoadResoureByName (bundleInfoList [i].Name);
						break;
					} 

				}
			}

		}
	}









	public void getLoadResoureByName(string name)
	{
		isLoading = true;
		name = name.ToLower ();

		AssetBundleItem _item=null;
		for (int i = 0; i < bundleInfoList.Count; i++) {
			if (bundleInfoList [i].Name == name) 
			{
				_item=bundleInfoList[i];
				break;
			}
				
		}
		if (_item!=null) 
		{
			LoadAssetBundle.Instance.LoadGameObject (_item, delegate(GameObject go)
			{
				go.transform.SetParent(this.transform);
				go.name=_item.Name;
					_item.isLoaded=true;
			    isLoading = false;
				
			});

		}

	}
	#endregion


	#region  test function
	void OnGUI1()
	{
		if (GUILayout.Button ("loaddd"))
		{
			getLoadResoureByName ("Bridge.prefab");

		}
	}


	#endregion

}
