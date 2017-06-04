using UnityEngine;
using System.Collections;

public class CancleBoxcolliders : MonoBehaviour 
{


	void Start () {
	
	}
	

	void Update () {
	
	}


	[ContextMenu("cancel renders")]
	public void cancelRenders(){
		Debug.Log ("Cancel......");
		foreach(Transform t in transform){
			MeshRenderer msr = t.gameObject.GetComponent<MeshRenderer> ();
			if (msr != null) {
				msr.enabled = false;
			}
		}
	}

	[ContextMenu("set renders")]
	public void setRenders(){
		Debug.Log ("Cancel......");
		foreach(Transform t in transform){
			MeshRenderer msr = t.gameObject.GetComponent<MeshRenderer> ();
			if (msr != null) {
				msr.enabled = true;
			}
		}
	}
}
