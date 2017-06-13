using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace MyGame.Two{


public class GenerateAI :NetworkBehaviour
{

	public GameObject prefab;
	public AIFactory aiFactory;
	public int enemyCount=6;
	void Start()
	{
		Init ();
	}

	private void Init()
	{
		if (aiFactory == null)
			aiFactory = new AIFactory ();
	}

	int range1=50;
	public override void OnStartServer ()
	{
		Debug.Log ("server..."+Time.time);
		Init ();
		for (int i = 0; i < enemyCount; i++) 
		{
			GameObject go = GameObject.Instantiate (prefab);
			go.transform.position = new Vector3 (Random.Range(-range1,range1),1,Random.Range(-range1,range1));
			NetworkServer.Spawn (go);
		}
		SoftSetUp.Instance.CurrentSenceEnemyCount += 6;
	}

		public void generateAI(int count){
			for (int i = 0; i < count; i++) 
			{
				GameObject go = GameObject.Instantiate (prefab);
				go.transform.position = new Vector3 (Random.Range(-range1,range1),1,Random.Range(-range1,range1));
				NetworkServer.Spawn (go);
			}
			SoftSetUp.Instance.CurrentSenceEnemyCount += count;
		}
}


}