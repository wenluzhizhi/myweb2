using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TestLoadImage : MonoBehaviour
{

	public RawImage showImage;

	private string rootPaht = "http://47.93.232.239/resources/";
	void Start ()
    {
	   
	}
	void Update ()
    {
        if (isRunning == false&&Time.time>2.0)
        {
            if (lis.Count < max)
            {
                StartCoroutine(loadImg(lis.Count+1,null));
            }
        }
	}
	int k=0;
	int max=11;
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
		WWW w1 = new WWW (rootPaht + "00"+k+".jpg");
	    yield return w1;
		Texture t = w1.texture;
        lis.Add(t);
        if (ac != null) ac(k-1);
        isRunning = false;
    }
}
