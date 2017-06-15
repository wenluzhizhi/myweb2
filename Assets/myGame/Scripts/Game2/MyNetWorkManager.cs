using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;



namespace MyGame.Two
{
   public class MyNetWorkManager:NetworkManager
		{
			//public string serverIP="localhost";
			//public int portIP = 7777;
		

			public GameObject softUp;

			public override void OnStopClient ()
			{
				base.OnStopClient ();
				/*
				if (ClientScene.localPlayers.Count > 0) 
				{
					SoftSetUp.Instance.RemovePlayer (ClientScene.localPlayers[0].gameObject.GetComponent<PlayerController>);
				}
			Debug.Log ("OnStopClient ---"+Time.time);
			*/
			}



		public override void OnStartClient (NetworkClient client)
		{
			base.OnStartClient (client);
			MainUIController.Instance.finishLaunchGame ();
		}
			public override void OnStartServer ()
			{
				base.OnStartServer ();
			    MainUIController.Instance.finishLaunchGame ();
				Invoke ("generatteSetup", 3.0f);
					
			}
			public void generatteSetup ()
			{

				if (softUp == null && NetworkServer.active) {
					softUp = GameObject.Instantiate (NetworkManager.singleton.spawnPrefabs [4]) as GameObject;
					softUp.gameObject.name = "softSetup";
					NetworkServer.Spawn (softUp);

				}
			}

		}
		
}
