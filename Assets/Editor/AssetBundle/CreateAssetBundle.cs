using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAssetBundle
{
    [MenuItem("AssetBundle/SetNames")]
    static void SetAssetBundleName() {
        Debug.Log("setName:"+Time.time);
        Object[] _selectObjects = Selection.GetFiltered(typeof(Object),SelectionMode.Assets);
        for (int i = 0; i < _selectObjects.Length; i++)
        {
            AssetImporter _aim = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(_selectObjects[i]));
            _aim.assetBundleName = AssetDatabase.GetAssetPath(_selectObjects[i]);
            _aim.assetBundleVariant = "unity3d";
        }
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }

   

    [MenuItem("AssetBundle/CreateBundle")]
    static void CreateBundle() {
        Debug.Log("Create Bundles");
		BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/Bundles/",BuildAssetBundleOptions.CompleteAssets,BuildTarget.WebGL);
    }

}
