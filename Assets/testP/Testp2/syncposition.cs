using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class syncposition : NetworkBehaviour
{


	[SyncVar]
	public Vector3 synPos = Vector3.zero;


	void Update(){

		if (isLocalPlayer) {

			commitd ();

		} else {

			transform.position = Vector3.Lerp (transform.position, synPos, 4*Time.deltaTime);
		}
	}



	[ClientCallback]
	private void  commitd(){
		CmdUploadVector (transform.position);
	}


	[Command]
	public void CmdUploadVector(Vector3 v){

		synPos = v;
	}

}
