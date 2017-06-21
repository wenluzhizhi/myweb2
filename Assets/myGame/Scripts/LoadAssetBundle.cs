using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;



public enum AssetBundleType
{
	GameObject,
Mat,
img,
}

public class AssetBundleItem
{
	public string Name;
	public string Path;
	public bool isHaveDependency;
	public bool immediately;
	public string GameName;
	public AssetBundleType assetType;
	public bool isLoaded;

	public string ToString ()
	{
		return string.Format ("Name={0} ,Path={1} ,isHaveDependency={2} ,immediately={3} ,GameName={4} ," +
			"assetType={5} ",
			Name, Path, isHaveDependency, immediately,GameName,assetType);
	}
}




public class LoadAssetBundle : MonoBehaviour
{
	#region singleton

	private static LoadAssetBundle _instance;

	public static LoadAssetBundle Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(LoadAssetBundle)) as LoadAssetBundle;
				if (_instance == null) {
					GameObject obj = new GameObject (typeof(LoadAssetBundle).ToString ());
					_instance = obj.AddComponent<LoadAssetBundle> ();
				}
			}
			return _instance;
		}
	}

	#endregion


	public static void MyLog (string str)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			Debug.Log (str);
		}
	}


	public string BundleRootPath = "http://47.93.232.239/AssetBundles/test/Bundle/";
	//public string BundleRootPath = "http://47.93.232.239/AssetBundles/Bundle/";
	//public  string BundleRootPath = "file:///"+Application.dataPath+"/StreamingAssets/bundle/";


	private AssetBundleManifest assetManifest = null;
	private string immediatelyLoadPath = "/initialsence/";
	private string assetBundlePostfix = ".unity3d";
	private string assetBundlePrePath = "assets/prefabsAssetBundle/";
	private int BundleVersion = 11;


	public void getAllAssets (string gameName,List<AssetBundleItem> bundleInfoList, string manifestName, Action ac)
	{
		StartCoroutine (loadManifest (gameName,bundleInfoList, BundleRootPath + manifestName, ac));
	}

	IEnumerator loadManifest (string gameName,List<AssetBundleItem> bundleInfoList, string path, Action ac)
	{

		using (WWW _w = WWW.LoadFromCacheOrDownload (path, BundleVersion)) {
			yield return _w;
			if (_w.error == null)
			{
				MyLog ("manifest...www...get success");
				AssetBundle asb = _w.assetBundle;
				assetManifest = asb.LoadAsset ("AssetBundleManifest") as AssetBundleManifest;
				if (assetManifest == null) 
				{
					MyLog ("manifest ... load ...failed");
				} 
				else 
				{
					
					bundleInfoList.Clear ();
					string[] _bundleName = assetManifest.GetAllAssetBundles ();
					for (int i = 0; i < _bundleName.Length; i++) 
					{

						_bundleName [i] = _bundleName [i].ToLower ();
						AssetBundleItem _item=new AssetBundleItem();	
						#region get isHaveDependency
						if (assetManifest.GetAllDependencies (_bundleName [i]) != null &&
						     assetManifest.GetAllDependencies (_bundleName [i]).Length > 0)
							_item.isHaveDependency = true;
						else
							_item.isHaveDependency = false;
						#endregion
						    
						#region getPath
						StringBuilder _str = new StringBuilder (_bundleName [i]);
						if (_str.Length == 0)
							continue;
						_item.Path = _str.ToString ();
						#endregion
						    
						#region  getisImmediatelyLoadPath
						if (_str.ToString ().Contains (immediatelyLoadPath))
							_item.immediately = true;
						else
							_item.immediately = false;
						
						#endregion


						#region getName
						getAssetBundleValidPath (_str); 
						if (_str.Length == 0)
							continue;
						int _slashPos = _str.ToString ().LastIndexOf ("/");
						int _totalCount = _str.Length;
						string _assetBundleShortName = _str.ToString ().Substring (_slashPos + 1, _totalCount - _slashPos - 1);
						_item.Name = _assetBundleShortName;
						#endregion


						#region  getGameName

						int _slahPosfirst = _str.ToString ().IndexOf ("/");
						string _stringGameName = _str.ToString ().Substring (0, _slahPosfirst);
						_item.GameName = _stringGameName;


						#endregion

						#region  getAssetBundleType
						if (_str.ToString ().EndsWith (".prefab"))
						{
							_item.assetType = AssetBundleType.GameObject;
						} 
						else if (_str.ToString ().EndsWith (".mat")) 
						{
							_item.assetType = AssetBundleType.Mat;
						}

						#endregion

						_item.isLoaded = false;
						if (_item.GameName.Equals (gameName)) {
							bundleInfoList.Add (_item);
						}

					}

					if (ac != null)
						ac ();
					
				}

				asb.Unload (false);
				asb = null;
				Resources.UnloadUnusedAssets ();
						
			} 
			else 
			{
				MyLog ("manifest...www...get failed" + _w.error);
			}
		}


	}



	public void LoadGameObject (AssetBundleItem _item,Action<GameObject> ac)
	{
		StartCoroutine (loadGameObject (_item,ac));
		
	}

	private IEnumerator loadGameObject (AssetBundleItem _item,Action<GameObject> ac)
	{
		using (WWW w1 = WWW.LoadFromCacheOrDownload (BundleRootPath + _item.Path, BundleVersion)) 
		{
			yield return w1;
			if (w1.error == null) 
			{
				AssetBundle _asb = w1.assetBundle;
				if (_asb != null) 
				{
					GameObject go = GameObject.Instantiate (_asb.LoadAsset (_item.Name) as GameObject);
					if (ac != null) {
						ac (go);
					}
				}
				_asb.Unload (false);

			} 
			else
			{
				MyLog ("" + w1.error);
			}
			
		}
	}



	private StringBuilder getAssetBundleValidPath (StringBuilder assetBundleName)
	{
		StringBuilder _str = assetBundleName;
		if (!_str.ToString ().EndsWith (assetBundlePostfix.ToLower()))
			return null;
		if (!_str.ToString ().StartsWith (assetBundlePrePath.ToLower()))
			return null;
		_str.Replace (assetBundlePrePath.ToLower(), "");
		_str.Replace (assetBundlePostfix.ToLower(), "");
		if (_str.Length == 0)
			return null;
		return _str;
		
		
	}

}
