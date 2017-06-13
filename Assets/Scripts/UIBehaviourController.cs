using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using MyGame.Two;


namespace MYWEB{



	[Serializable]
	public class MyWebMainPageItemConfig{

		public int imgeCount=0;
		public List<itemConfig> lis = new List<itemConfig> ();
	}

	[Serializable]
	public class itemConfig{

		public itemConfig(string name,string path){
			this.name = name;
			this.path = path;
		}
		public string name = "";
		public string path = "";
	}

public class UIBehaviourController : MonoBehaviour 
{

    
	public GameObject itemPrefab;
	private string configName="center_show_img.txt";
	private int childImageCount=0;
		public Sprite[] iconSprites=new Sprite[]{};
   public ScrollRect scrollRect;
		public Scrollbar scrollbar;
		MyWebMainPageItemConfig _my1;
		public Image bigImage;
	void Start(){
	     	
			itemPrefab.SetActive (false);
			StartCoroutine(loadConfigFile());
	}

		private void InitConfigFile()
		{
			/*
			MyWebMainPageItemConfig myc = new MyWebMainPageItemConfig ();
			myc.lis.Add (new itemConfig("img1","img1"));
			myc.lis.Add (new itemConfig("img2","img2"));

			myc.imgeCount = myc.lis.Count;
			string de1=JsonUtility.ToJson (myc);

			File.WriteAllText (configName,de1);
			Debug.Log (de1);
			*/
		}

		#region  //加载配置文件


		IEnumerator loadConfigFile()
		{
			WWW w1 = new WWW (WebConfig.MyWebRoot+WebConfig.CenterLoopImgPath+configName);
			yield return w1;
			if (w1 != null) 
			{
				_my1=JsonUtility.FromJson< MyWebMainPageItemConfig> (w1.text);
				InitChildImage(_my1);
			}
		}

		/// <summary>
		/// Inits the child image.
		/// </summary>根据从网络端读取的配置信息进行初始化列表
		private void InitChildImage(MyWebMainPageItemConfig _my1)
		{
			for (int i = 0; i < _my1.lis.Count; i++) 
			{
				GameObject go = GameObject.Instantiate (itemPrefab) as GameObject;
				go.name = i + "";

				if (i < iconSprites.Length)
					go.gameObject.GetComponent<Image> ().sprite = iconSprites [i];
				else
					go.gameObject.GetComponent<Image> ().sprite = iconSprites [iconSprites.Length - 1];


				go.SetActive (true);
				go.transform.SetParent (itemPrefab.gameObject.transform.parent,false);
			}
		}




		#endregion


	#region onclickEvent

	public void OnClickNextButton(){

			scrollbar.value += 0.1f;

	}
	public void OnClickPreviousButton(){
			scrollbar.value -= 0.1f;
	}

	
	public int currentNum=-1;
	public void OnClickItem(GameObject go)
	{
		int strNum = int.Parse(go.name);
			if (strNum < _my1.imgeCount) {
				StartCoroutine (loadBigItem(_my1.lis[strNum].path));
				currentNum = strNum;
			}
			
	}

		public void OnClickBigItem(){
			if (currentNum < _my1.imgeCount)
			{
				if (currentNum == 0) {
					Application.LoadLevel ("water");
				} else if (currentNum == 1) {
					Application.LoadLevel ("main");
				}

				else if (currentNum == 2) {
					Application.LoadLevel ("game_main");
				}
			}

		}

	
		IEnumerator loadBigItem(string name)
		{
			WWW w1 = new WWW (WebConfig.MyWebRoot+WebConfig.CenterLoopImgPath+name);
			yield return w1;
			if (w1 != null) 
			{
				Texture2D t = w1.texture as Texture2D;
				Sprite sp=Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0,0));
				bigImage.sprite = sp;

				float realImgeRatio = (float)t.width / (float)t.height;
				Vector2 size = bigImage.rectTransform.rect.size;

				bigImage.rectTransform.sizeDelta = new Vector2 (realImgeRatio*size.y,size.y);

			}
		}


	
	#endregion
}


}