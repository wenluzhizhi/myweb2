using UnityEngine;
using System.Collections;

public class PlayerCp1 : MonoBehaviour {


	void Start () {

	}
	
	public float h=0.0f;
	public float v = 0.0f;
	void Update () {
	   

		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		transform.Translate (transform.forward*v*Time.deltaTime,Space.Self);
		transform.Rotate (new Vector3 (0,h,0));
	}
}
