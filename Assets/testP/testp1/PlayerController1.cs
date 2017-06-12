using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController1:NetworkBehaviour
{

	public float traSpeed=3;
	public float rotSpeed=120;
	public Transform bulletPos;

	float h=0.0f;
	float v=0.0f;

	public  GameObject bulletPrefab;

	void Update(){

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Cmdfire ();
		}

		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		transform.Translate (Vector3.forward*v*Time.deltaTime*traSpeed);
		transform.Rotate (Vector3.up*h*rotSpeed*Time.deltaTime);

	}


	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	[Command]
	void Cmdfire(){
		GameObject go = GameObject.Instantiate (bulletPrefab,bulletPos.position,Quaternion.identity) as GameObject;
		go.gameObject.GetComponent<Rigidbody> ().velocity =transform.forward * 30;
		Destroy (go,2);
		NetworkServer.Spawn (go);
	}


}
