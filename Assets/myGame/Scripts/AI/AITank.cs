using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AITank : NetworkBehaviour,AIEnemy 
{
	public void Attack(){

		//CmdAttack ();
	}
	public void Sleep(){}


	public void Front()
	{
		

	}


	private int sleepThreshold = 5;
	private int AwakesleepThreshold = 95;
	private bool isSleep=false;
	public int random0_100 = 0;
	public Transform bulletGeneratePos;
	public GameObject Bullet;
	void  Update()
	{
		random0_100 = Mathf.Abs(Utils.getRandom1() % 100);
		if (random0_100 < sleepThreshold)
		{
			if (isSleep == false) {
				isSleep = true;
				this.transform.Rotate(new Vector3(0,random0_100%45,0));
			}

		}
		if (random0_100 > AwakesleepThreshold) {
			if (isSleep)
				isSleep = false;
		}

		if (random0_100 > 95) {
			//Attack ();
			if(NetworkManager.singleton.isNetworkActive){
				CmdAttack1();
			}

		}


		if(!isSleep)
		  this.transform.Translate (transform.forward*Time.deltaTime,Space.World);

	}


   [Command]
	public void CmdAttack1()
	{
		GameObject _bullet = GameObject.Instantiate (Bullet,bulletGeneratePos.position,Quaternion.identity) as GameObject;
		_bullet.gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * 60;
		Destroy (_bullet,2);
		//NetworkServer.Spawn (_bullet);
	}




}
