using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestLoadImage : MonoBehaviour
{

	public RawImage showImage;

	private string rootPaht = "http://47.93.232.239/resources/";
	void Start () {
	
	}
	

	void Update () {
	
	}



	int k=0;
	int max=11;
	public void OnClickNextBtn()
	{
		k++;
		if (k > max)
			k = max;
		StartCoroutine (loadImg (k));
	}

	public void OnClickPrevious()
	{
		k--;
		if (k < 1)
			k = 1;
		StartCoroutine (loadImg (k));	
	}


	IEnumerator loadImg(int k){
		WWW w1 = new WWW (rootPaht + "00"+k+".jpg");
	    yield return w1;
		Texture t = w1.texture;
		showImage.texture = t;

	}
}
