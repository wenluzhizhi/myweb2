using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;

namespace MYWEB{

	[Serializable]
	public class MyImgConfig{
		public	int maxCount=12;
	}

public class TestLoadImage : MonoBehaviour
{

	public RawImage showImage;

	private string rootPath = "http://47.93.232.239/resources/";
	private string rootPath_myWeb = "http://47.93.232.239/myweb/";

		private string fileName="MyImageConfig.txt";


    bool isStart=false;
	void Start ()
    {       
			/*
			MyImgConfig myconfig = new MyImgConfig ();
			myconfig.maxCount = 12;

			string json1 = JsonUtility.ToJson (myconfig);
			Debug.Log (json1);


			File.WriteAllText ("11.txt", json1);
			*/

			StartCoroutine (getConfigFile());
	}
	

		IEnumerator getConfigFile(){

			WWW w1 = new WWW (rootPath + fileName);
			yield return w1;

			if (w1.error == null) {

				MyImgConfig my1 = JsonUtility.FromJson<MyImgConfig> (w1.text) as MyImgConfig;
				//Debug.Log (my1.maxCount);
				max = my1.maxCount;
				isStart = true;
			}

		}



	void Update ()
    {
			if (isRunning == false&&Time.time>2.0&&isStart)
        {
            if (lis.Count < max)
            {
                StartCoroutine(loadImg(lis.Count+1,null));
            }
        }
	}

	#region  OnClick
	int k=0;
	int max=21;
	public void OnClickNextBtn()
	{
		k++;
		if (k > max)
			k = max;
        OpenPic(k);
    }

	public void OnClickPrevious()
	{
		k--;
		if (k < 1)
			k = 1;
        OpenPic(k);
	}


	public void OnClickMyWeb(){
        /*
		Application.OpenURL (rootPath_myWeb);
		*/
			Application.LoadLevel ("myWeb");
	}

	#endregion


    public List<Texture> lis = new List<Texture>();
    public void OpenPic(int k)
    {
        if (lis.Count>=k)
        {
            ShowImage(k-1);
        }
        else
        {
            if (!isRunning)
                StartCoroutine(loadImg(k, ShowImage));
            else
            {
                if (k >= 1)
                    k = k - 1;
            }
               
        }
    }

    private void ShowImage(int num)
    {
        if(lis.Count>num)
          showImage.texture = lis[num];
    }


    bool isRunning = false;
	IEnumerator loadImg(int k,Action<int> ac)
    {
        isRunning = true;
		WWW w1 = new WWW (rootPath + "00"+k+".jpg");
	    yield return w1;
		Texture t = w1.texture;
        lis.Add(t);
        if (ac != null) ac(k-1);
        isRunning = false;
    }
}


}