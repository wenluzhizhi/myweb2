using UnityEngine;
using System.Collections;

public class testP30613 : MonoBehaviour {

	public Transform target;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	   
		Vector3 v = target.position - transform.position;
		float an = Vector3.Angle (transform.forward,new Vector3(v.x,0,v.z));

		transform.Rotate (0,an,0);
	}
}
