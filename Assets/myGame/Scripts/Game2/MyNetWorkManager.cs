using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;



namespace MyGame.Two{

namespace MyGame_sence
{

	public class MyNetWorkManager:NetworkManager
    {
		//public string serverIP="localhost";
		//public int portIP = 7777;
		

			void Start(){
				this.gameObject.transform.SetSiblingIndex (1);
			}

		public override void OnClientDisconnect (NetworkConnection conn)
		{

			base.OnClientDisconnect (conn);
			Debug.Log ("OnClientDisconnect---"+Time.time);
		}


		public override void OnClientConnect (NetworkConnection conn)
		{
			base.OnClientConnect (conn);
			Debug.Log ("OnClientConnect---"+Time.time);

		}


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


		public override void OnClientError (NetworkConnection conn, int errorCode)
		{
			base.OnClientError (conn,errorCode);
			Debug.Log ("OnClientError---"+Time.time);

		}


		public override void OnServerDisconnect (NetworkConnection conn)
		{
			base.OnServerDisconnect (conn);
			Debug.Log ("OnServerDisconnect---"+Time.time+"+-"+conn.hostId);
		}
		public override void OnServerConnect (NetworkConnection conn)
		{
			base.OnServerConnect (conn);
			Debug.Log ("OnServerConnect---"+Time.time+"+-"+conn.hostId);


			List< UnityEngine.Networking.PlayerController> list1 = conn.playerControllers;
			if (list1 == null || list1.Count == 0)
				Debug.Log ("lianjiezo----------null--");
			for (int i = 0; i < list1.Count; i++)
			{
				Debug.Log (list1[i].gameObject.name);
			}
		}


		void Update()
		{
			if (Input.GetKeyDown (KeyCode.N)) {

				List< UnityEngine.Networking.PlayerController> list1=ClientScene.localPlayers;
				for (int i = 0; i < list1.Count; i++) {
					Debug.Log (list1[i].gameObject.name);
				}
			}
		}
    }

}


}
