using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpaw : NetworkBehaviour{

	public GameObject enemyPrefab;
	public int enemycount=6;


	public override void OnStartServer ()
	{

		for (int i = 0; i < enemycount; i++) {

			Vector3 ePosition = new Vector3 (Random.Range(-8,8f),0,Random.Range(-8,8));
			Quaternion eRotation = Quaternion.Euler (0,Random.Range(0,360),0);
			GameObject go = GameObject.Instantiate (enemyPrefab,ePosition,eRotation) as GameObject;
			NetworkServer.Spawn (go);

		}
	}



}
