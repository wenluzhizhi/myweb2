using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerBulletController :NetworkBehaviour
{

	public PlayerController owner;
	void OnCollisionEnter(Collision info)
	{

		if (!isServer)
			return;
		if (info.gameObject.CompareTag (WebConfig.tagName_tag_tank)) 
		{
			owner.PlayerScore+=1;
			Destroy (info.gameObject);
			Destroy (gameObject);
		}
	}

}
