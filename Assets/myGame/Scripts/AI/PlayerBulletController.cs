﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace MyGame.Two
{


	public class PlayerBulletController :NetworkBehaviour
	{
		public PlayerController owner;

		void OnCollisionEnter (Collision info)
		{
			if (!isServer)
				return;
			if (info.gameObject.tag == WebConfig.tagName_tag_tank) 
			{
				owner.PlayerScore += 1;
			    SoftSetUp.Instance.currentSenceEnemyCount -= 1;
				SoftSetUp.Instance.geneTankAI ();
				Destroy (info.gameObject);
				Destroy (gameObject);
			}
		}

	}


}