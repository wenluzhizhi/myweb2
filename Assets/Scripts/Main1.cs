using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Main1 : MonoBehaviour 
{


	public RawImage mm1;
	public RectTransform content;
	void Start () {
		load1 ();
	}
	
    
	private void load1(){

	   Texture [] sps = Resources.LoadAll<Texture> ("pic");

		for (int i = 0; i < sps.Length; i++) {
			

			GameObject go = GameObject.Instantiate (mm1.gameObject) as GameObject;
			go.transform.SetParent (content);
			go.GetComponent<RawImage>().texture=sps[i];
		}
	}
}
