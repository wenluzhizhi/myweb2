using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace MyGame.Two
{


	public class AITank : NetworkBehaviour,AIEnemy
	{
		public void Attack ()
		{


		}

		public void Sleep ()
		{
		}


		public void Front ()
		{
		

		}


		private int sleepThreshold = 10;
		private int AwakesleepThreshold = 90;
		private bool isSleep = false;
		public int random0_100 = 0;
		public Transform bulletGeneratePos;
		public GameObject Bullet;


		public Transform currentplayerTr;
		public float distance_player = 0.0f;
		public float checkTime = 10.0f;
		//check tank positon and orientation time internal
		public float checkTime_accumulation = 0.0f;
		public float allowTankMaxDistance = 200;

		public Vector3 lastPlayerOrientation = Vector3.zero;

		public float tank_forward_speed = 10;

		private void getLoacalPlayer ()
		{
			if (currentplayerTr == null)
			{
				currentplayerTr = SoftSetUp.Instance.GetFollowPlayer ();
	      	}
			
		}


		void Start ()
		{
		
		}

		void  Update ()
		{

			if (!isServer)
				return;
			checkTime_accumulation += Time.deltaTime;
			if (checkTime_accumulation > checkTime) {
				getLoacalPlayer ();
				Vector3 _centerPos = Vector3.up;
				if (currentplayerTr != null) {              
					_centerPos = currentplayerTr.transform.position;
				} 
				distance_player = Vector3.Distance (this.transform.position, _centerPos);
				if (distance_player > allowTankMaxDistance)
				{
					lastPlayerOrientation = _centerPos - this.transform.position;
					float _angle = Vector3.Angle (new Vector3 (lastPlayerOrientation.x, 0, lastPlayerOrientation.z), transform.forward);
					this.transform.Rotate (new Vector3 (0, _angle, 0));

				} else {
					lastPlayerOrientation = transform.forward;
				}
			}


			random0_100 = Mathf.Abs (Utils.getRandom1 () % 100);
			if (random0_100 < sleepThreshold) {
				if (isSleep == false) {
					isSleep = true;
					//this.transform.Rotate(new Vector3(0,random0_100%45,0));
				}

			}
			if (random0_100 > AwakesleepThreshold) {
				if (isSleep)
					isSleep = false;
			}

			if (random0_100 > 98) {
				if (NetworkManager.singleton.isNetworkActive) {  
					if (isServer) {
						RpcAttack ();
					}
				}
			}
			if (!isSleep)
				this.transform.Translate (transform.forward * Time.deltaTime * tank_forward_speed, Space.World);

		}





		[ClientRpc]
		public void RpcAttack ()
		{
			GameObject _bullet = GameObject.Instantiate (Bullet, bulletGeneratePos.position, Quaternion.identity) as GameObject;
			_bullet.gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * 60;
			Destroy (_bullet, 2);
		}




	}



}