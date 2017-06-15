using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace MyGame.Two{


public class GenerateAI :MonoBehaviour
{

	public GameObject prefab;
	public AIFactory aiFactory;

		public bool isMyServer=false;
		public bool isMyClient=false;
		public bool isMyLoaclPlayer=false;
	void Start()
	{
		Init ();
			Invoke ("generateTank", 3.0f);
	}

		private void generateTank()
		{
			if (NetworkServer.active) 
			{
				InitGenerateEnemy ();
			}
		}

	private void Init()
	{
		if (aiFactory == null)
			aiFactory = new AIFactory ();
	}

	public int EnemyRange=50;
	public  void InitGenerateEnemy ()
	{
		Debug.Log ("server..."+Time.time);
		Init ();
		for (int i = 0; i < 6; i++) 
		{
			GameObject go = GameObject.Instantiate (prefab);
				go.transform.position = new Vector3 (Random.Range(-EnemyRange,EnemyRange),1,Random.Range(-EnemyRange,EnemyRange));
			NetworkServer.Spawn (go);
		}
		SoftSetUp.Instance.currentSenceEnemyCount += 6;
	}

		public void SpawnTankAI(int count)
		{
			for (int i = 0; i < count; i++) 
			{
				GameObject go = GameObject.Instantiate (prefab);
				go.transform.position = new Vector3 (Random.Range(-EnemyRange,EnemyRange),1,Random.Range(-EnemyRange,EnemyRange));
				NetworkServer.Spawn (go);
			}
			SoftSetUp.Instance.currentSenceEnemyCount += count;
		}
}


}