using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyWeb_Game2 : MonoBehaviour
{


    public static void MyLog(string str) {
        if (Application.platform==RuntimePlatform.WindowsEditor) {
            Debug.Log(str);
        }
    }


    public string bundlePath = "http://47.93.232.239/AssetBundles/test/Bundle/";
    void Start()
    {
        StartCoroutine(LoadManifest());
    }


    public List<string> listAssets = new List<string>();
    private IEnumerator LoadManifest()
    {
        listAssets.Clear();
        string strManifestFileName = bundlePath + "Bundle.unity3d";
        MyLog(strManifestFileName);
        WWW wloadManifest = new WWW(strManifestFileName);
        yield return wloadManifest;
        if (wloadManifest.error == null)
        {
            MyLog("Manifest get sucess:"+Time.time);
            AssetBundleManifest _asf = wloadManifest.assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
            if (_asf != null) {
                string[] assetNames = _asf.GetAllAssetBundles();
                for(int i = 0; i < assetNames.Length; i++)
                {
                    if (!listAssets.Contains(assetNames[i]))
                    {
                        listAssets.Add(assetNames[i]);
                    }
                }
            }
        }
        else
        {
            MyLog("Manifest get failed:"+Time.time+strManifestFileName);
        }
        if (listAssets.Count > 0) {
            loadData();
        }
       
    }
    private void loadData()
    {

        for (int i = 0; i < listAssets.Count; i++)
        {
            StartCoroutine(loadPrefab(listAssets[i]));

        }
    }

    IEnumerator loadPrefab(string name)
    {

        string p1 = bundlePath + name;
        MyLog("loadprefab  load path:=" + p1);
        WWW w2 = new WWW(p1);
        yield return w2;

        if (name.Contains("/"))
        {
            int k = name.LastIndexOf("/");
            int total = name.Length;
            name = name.Substring(k + 1, total - k - 1);
            if (name.Contains(".unity3d"))
            {
                name = name.Replace(".unity3d", "");
            }
        }
        MyLog("name:" + name);
        if (w2.error == null)
        {
            MyLog("loadPrefab get data success" + Time.time);
            AssetBundle asb = w2.assetBundle;
            if (name.Contains(".prefab"))
            {
                if(asb.Contains(name))
                   GameObject.Instantiate(asb.LoadAsset(name) as GameObject);
            }
            else if (name.Contains(".mat")) {
                if (asb.Contains(name))
                {
                    Material m = asb.LoadAsset(name) as Material;
                }
            }
            else if (name.Contains(".png")|| name.Contains(".jpg"))
            {
                if (asb.Contains(name))
                {
                    Texture m = asb.LoadAsset(name) as Texture;
                }
            }
        }
        else
        {
            MyLog(w2.error+"  "+p1);
        }


    }
}
