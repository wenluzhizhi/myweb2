using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MYM :NetworkManager {

	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);
		Debug.Log ("OnClientConnect..."+Time.time);
	}

	public override void OnClientDisconnect (NetworkConnection conn)
	{
		base.OnClientDisconnect (conn);
	
		Debug.Log ("OnClientDisconnect...."+"  "+conn.ToString()+Time.time);
	}

	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
		Debug.Log ("OnclientError....."+Time.time);
	}

	public override void OnServerReady (NetworkConnection conn)
	{
		base.OnServerReady (conn);
		Debug.Log ("OnServerReady"+Time.time);
	}

	public override void OnServerDisconnect (NetworkConnection conn)
	{
		base.OnServerDisconnect (conn);
		Debug.Log ("OnServerDisconnect..."+Time.time);
	}
}
