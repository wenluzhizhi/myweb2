using UnityEngine;
using System.Collections;

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




}
