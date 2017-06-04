using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadAssetBundle : MonoBehaviour
{
    #region singleton
	private static LoadAssetBundle _instance;
	public static LoadAssetBundle Instance{
		get
		{
			if (_instance == null) 
			{
				_instance = GameObject.FindObjectOfType (typeof(LoadAssetBundle)) as LoadAssetBundle;
				if (_instance == null) {
					GameObject obj = new GameObject (typeof(LoadAssetBundle).ToString());
					_instance= obj.AddComponent<LoadAssetBundle> ();
				}
			}
			return _instance;
		}
	}
    #endregion


	public static void MyLog(string str)
	{
		if (Application.platform==RuntimePlatform.WindowsEditor)
		{
			Debug.Log(str);
		}
	}


	///public string BundleRootPath = "http://47.93.232.239/AssetBundles/test/Bundle/";
	public string BundleRootPath = "http://47.93.232.239/AssetBundles/Bundle/";
	//public  string BundleRootPath = "file:///"+Application.dataPath+"/StreamingAssets/bundle/";
	private AssetBundleManifest assetManifest = null;
	private string immediatelyLoadPath = "assets/prefabs/initialsence/";
	private string assetBundlePostfix = ".unity3d";
	private int BundleVersion=5;
	public void getAllAssets(List<AssetBundleItem> bundleInfoList,string manifestName,Action ac){
		StartCoroutine (loadManifest(bundleInfoList,BundleRootPath+manifestName,ac));
	}

	IEnumerator loadManifest(List<AssetBundleItem> bundleInfoList,string path,Action ac){
		
		using(WWW _w=WWW.LoadFromCacheOrDownload(path,BundleVersion))
		{
			yield return _w;
			if (_w.error == null)
			{
				MyLog ("manifest...www...get success");
				AssetBundle asb = _w.assetBundle;
				assetManifest =asb.LoadAsset ("AssetBundleManifest") as AssetBundleManifest;
				if (assetManifest == null) {
					MyLog ("manifest ... load ...failed");
				} 
				else
				{
					
						bundleInfoList.Clear ();
						string[] _bundleName = assetManifest.GetAllAssetBundles ();
						for (int i = 0; i < _bundleName.Length; i++) 
					    {
							AssetBundleItem _item;
							string _str = _bundleName [i];
							if (string.IsNullOrEmpty (_str))
								continue;

							int _slashPos = _str.LastIndexOf ("/");
							int _totalCount = _str.Length;
							string _assetBundleShortName = _str.Substring (_slashPos+1,_totalCount-_slashPos-1);
							if (_assetBundleShortName.Contains (assetBundlePostfix)) {
							   _assetBundleShortName=_assetBundleShortName.Replace (assetBundlePostfix,"");
							}
							_item.Name = _assetBundleShortName;
							_item.Path = _str;

						if (assetManifest.GetAllDependencies (_bundleName [i]) == null ||
						    assetManifest.GetAllDependencies (_bundleName [i]).Length > 0)
							_item.isHaveDependency = true;
						else
							_item.isHaveDependency = false;
							
							if (_str.Contains (immediatelyLoadPath))
								_item.immediately = true;
							else
								_item.immediately = false;

						    bundleInfoList.Add (_item);

							
						}

						if (ac!=null)
							ac ();
					
				}

				asb.Unload (false);
				asb = null;
				Resources.UnloadUnusedAssets ();
						
			} 
			else 
			{
				MyLog ("manifest...www...get failed"+_w.error);
			}
		}


	}



	public void LoadGameObject(AssetBundleItem _item){
		StartCoroutine (loadGameObject(_item));
		
	}

	private IEnumerator loadGameObject(AssetBundleItem _item){
		using(WWW w1=WWW.LoadFromCacheOrDownload(BundleRootPath+_item.Path,BundleVersion))
		{
			yield return w1;
			if (w1.error == null) 
			{
				AssetBundle _asb = w1.assetBundle;
				if (_asb != null) {
					GameObject go = GameObject.Instantiate (_asb.LoadAsset(_item.Name) as GameObject);
				}
				_asb.Unload (false);

			} else {
				MyLog (""+w1.error);
			}
			
		}
	}


}
