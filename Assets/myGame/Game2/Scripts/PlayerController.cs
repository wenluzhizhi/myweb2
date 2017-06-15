using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace MyGame.Two
{


	public class PlayerController:NetworkBehaviour
	{


		public Material MainMa;
		private Animator animator;
		private BoxCollider boxcollider;
		private Rigidbody rigidbody;

		private AnimatorStateInfo animatorState = new AnimatorStateInfo ();
		public int rotateSpeed = 20;
		public int thrustSpeed = 20;

		[SerializeField]
		private GameObject bulletPrefab;
		[SerializeField]
		private Transform shootBulletPos;


		private BoxCollider playerBoxcollider;

		[SyncVar (hook = "OnScoreChange")]
		public int PlayerScore;

		void Start ()
		{
			animator = this.gameObject.GetComponent<Animator> ();
			rigidbody = this.gameObject.GetComponent<Rigidbody> ();
			playerBoxcollider = this.gameObject.GetComponent<BoxCollider> ();
			playerBoxcollider.center = new Vector3 (0, 0.7f, 0);
			if (isLocalPlayer) 
			{

				this.gameObject.GetComponent<CameraFollow> ().enabled = true;
				this.gameObject.name = "MainPlayer";

			} else {
				this.gameObject.GetComponent<CameraFollow> ().enabled = false;
				this.gameObject.name = "FriendPlayer";

			}

			if (isServer) {

				if (SoftSetUp.Instance != null) {
					SoftSetUp.Instance.AddPlayer (this);
				} else {
					Debug.Log ("---softSetup...Null" + Time.time);
					Invoke ("AddPlayerToServer", 4.0f);

				}
			}
		
		}

		private void AddPlayerToServer ()
		{
			if (SoftSetUp.Instance != null) {
				SoftSetUp.Instance.AddPlayer (this);
			} 
		}





		void Update ()
		{



			if (!isLocalPlayer)
				return;
			animatorState = animator.GetCurrentAnimatorStateInfo (0);
			if (Input.GetKey (KeyCode.UpArrow)) {
				playerForward (1);	
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				playerForward (-1);
			} else {
				if (animatorState.IsName ("1HWalkF")) {
					animator.SetBool (WebConfig.animator_state_walk, false);
				}
				animator.SetBool (WebConfig.animator_state_idle, true);
			}

			if (Input.GetKey (KeyCode.LeftArrow)) {
				playerRotateRight (-5);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				playerRotateRight (5);
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				if (this.transform.position.y < 10) {
					this.rigidbody.velocity = new Vector3 (0,10,0);
					PlayJump ();
				}
            
			} else {
				StopJump ();
			}
			if (Input.GetKeyDown (KeyCode.J)) {
				PlayAttack ();
			
				CmdshootBullet ();
			

			} else {
				StopAttack ();
			}
		}

	



		[Command]
		public void  CmdshootBullet ()
		{
			GameObject _bullet = GameObject.Instantiate (bulletPrefab, shootBulletPos.position, Quaternion.identity) as GameObject;
			_bullet.gameObject.SetActive (true);
			_bullet.gameObject.GetComponent<PlayerBulletController> ().owner = this;
			_bullet.gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * 30;
			Destroy (_bullet, 2);
			NetworkServer.Spawn (_bullet);
		}




		public void PlayJump ()
		{
			if (!animatorState.IsName ("Idle_JumpDownMed_Idle")) {
				animator.SetBool (WebConfig.animator_state_jump, true);
			}
		}

		public void StopJump ()
		{
			if (animatorState.IsName ("Idle_JumpDownMed_Idle")) {
				animator.SetBool (WebConfig.animator_state_jump, false);
			}
		}


		public void PlayAttack ()
		{
			if (!animatorState.IsName ("2HAttack")) {
				animator.SetBool (WebConfig.animator_state_attack, true);
			}
		}

		public void StopAttack ()
		{
			if (animatorState.IsName ("2HAttack")) {
				animator.SetBool (WebConfig.animator_state_attack, false);
			}
		}





		private void playerForward (int ratio)
		{
			transform.Translate (Vector3.forward * Time.deltaTime * ratio * thrustSpeed);
			wald ();
        

		}

		public void wald ()
		{
			if (!animatorState.IsName ("1HWalkF")) {
				animator.SetBool (WebConfig.animator_state_walk, true);
				animator.SetBool (WebConfig.animator_state_idle, false);
			}

		}

		private void playerRotateRight (int ratio)
		{
			transform.Rotate (new Vector3 (0, Time.deltaTime * rotateSpeed * ratio * rotateSpeed, 0));
		}




		public void OnScoreChange (int k)
		{
			if (isLocalPlayer) {
				MainUIController.Instance.SetPlayerScore (k);
			}
		}


		void OnCollisionEnter1 (Collision c)
		{
			if (c.gameObject.tag == WebConfig.tagName_tank_bullet) {
				if (isLocalPlayer) {
					PlayerScore -= 1;	
				}
				Destroy (c.gameObject);
			}
		}



		void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.tag == WebConfig.tagName_tank_bullet) 
			{
				if (isServer) 
				{
					PlayerScore -= 1;
					SetColor ();
				}
				Destroy (c.gameObject);
			}
		}


		int kcount=1;
		public void SetColor(){
			if (MainMa != null && kcount == 1) {
				MainMa.color = Color.red;
				kcount = 2;
			} else {
				MainMa.color = Color.white;
				kcount = 1;
			}
		}
	


	}


}