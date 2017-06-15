using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{


	public Transform cameraTrasform;
	public Vector3 OffsetPosition;
	void Start ()
	{
		cameraTrasform = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform;


		cameraTrasform.SetParent (this.gameObject.transform);
		cameraTrasform.localPosition = new Vector3 (0,2,3);
		cameraTrasform.rotation = Quaternion.Euler (10,180,0);


		OffsetPosition = cameraTrasform.position - this.transform.position;
		cameraTrasform.SetParent (null);
		cameraTrasform.LookAt (transform.position);


	}
	

	void Update () {
		
		RotateView ();
		scrollView ();
		cameraTrasform.position = OffsetPosition + this.transform.position;
		cameraTrasform.LookAt (transform.position);
	}


	private float distance;
	public float ScrollSpeed=1.0f;
	private void scrollView()
	{
		distance = OffsetPosition.magnitude;
		distance -= Input.GetAxis ("Mouse ScrollWheel")*ScrollSpeed;
		OffsetPosition = OffsetPosition.normalized * distance;


		//cameraTrasform.position = OffsetPosition + this.transform.position;
	}
	private bool isRotating=false;
	public  float rotateSpeed=1.0f;
	private void RotateView()
	{
		//鼠标右键按下可以旋转视野
		if (Input.GetMouseButtonDown(0))
			isRotating = true;
		if (Input.GetMouseButtonUp(0))
			isRotating = false;

		if (isRotating)
		{
			Vector3 originalPosition = cameraTrasform.position;
			Quaternion originalRotation =cameraTrasform.rotation;
			cameraTrasform.RotateAround(transform.position, transform.up, rotateSpeed * Input.GetAxis("Mouse X"));
			cameraTrasform.RotateAround(transform.position, cameraTrasform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
			float x = cameraTrasform.eulerAngles.x;
			//旋转的范围为10度到80度
			if (x < 10 || x > 80)
			{
				cameraTrasform.position = originalPosition;
				cameraTrasform.rotation = originalRotation;
			}
			OffsetPosition =cameraTrasform.position - transform.position;


			//cameraTrasform.position = OffsetPosition + this.transform.position;
		}




	}
}
