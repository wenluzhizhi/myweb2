using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyWeb_Game1 : MonoBehaviour
{


	void Start ()
	{
	
	}
	

	void Update ()
	{
	
	}

	public string path="http://47.93.232.239/AssetBundles/Bundle/bundles.unity3d";
	void OnGUI(){

		if (GUILayout.Button ("load")) {
			StartCoroutine (f1());
		}


		//if(GUILayout.Button(""))
	}



	public List<string> list = new List<string> ();
	private void getDe(string name,AssetBundleManifest asf){
		
		if (asf.GetAllDependencies (name).Length > 0) {

			for (int i = 0; i < asf.GetAllDependencies (name).Length; i++) {
				
				getDe (asf.GetAllDependencies (name) [i], asf);
				if (!list.Contains (asf.GetAllDependencies (name) [i])) {
					list.Add (asf.GetAllDependencies (name) [i]);
				}
			}
		} 
		else
		{
			if (!list.Contains (name)) {
				list.Add (name);
			}
		}
	}

	public void StartLoad(){
		for (int i = 0; i < list.Count; i++) {
			
		}
	}

	IEnumerator f1()
	{

		WWW w1 = new WWW (path);
		yield return w1;
		if (w1.error == null) 
		{
			Debug.Log ("1-----");
			AssetBundle asb = w1.assetBundle;
			AssetBundleManifest asf = asb.LoadAsset ("AssetBundleManifest") as AssetBundleManifest;
			if (asf != null)
			{
				Debug.Log ("3----");
				string[] ast=asf.GetAllAssetBundles ();
				if (ast.Length > 0) 
				{
					for (int i = 0; i < ast.Length; i++) 
					{
						getDe (ast[i],asf);
						if (!list.Contains (ast [i])) {
							list.Add (ast[i]);
						}
					}
				}
				StartLoad ();
			} 
			else
			{
				Debug.Log ("2-----");
			}
		} 
		else
		{
			Debug.Log ("0---------"+w1.error);
		}
	}
}
