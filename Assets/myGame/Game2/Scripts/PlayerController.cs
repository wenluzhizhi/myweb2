using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour{


	private Transform player;
	private Animator animator;
	private BoxCollider boxcollider;
	private Rigidbody rigidbody;
	private AnimatorStateInfo animatorState = new AnimatorStateInfo ();
	public int rotateSpeed=20;
	public int thrustSpeed=20;
	void Start(){
		player = this.gameObject.GetComponent<Transform> ();
		animator = this.gameObject.GetComponent<Animator> ();
		rigidbody = this.gameObject.GetComponent<Rigidbody> ();


	}


	void Update(){

		if(!isLocalPlayer) return;
		animatorState = animator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			playerForward (1);	
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			playerForward (-1);
		} else {

			if (!animatorState.IsName ("DualIdle")&&!animatorState.IsName ("2HAttack")) {
				animator.SetTrigger (WebConfig.animator_state_idle);
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerRotateRight (-5);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRotateRight (5);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (this.transform.position.y < 5) {
				this.rigidbody.velocity = new Vector3 (0,5,0);
			}
		}
	}


	private void playerForward(int ratio){
		player.Translate (Vector3.forward*Time.deltaTime*ratio);
		if(!animatorState.IsName("1HWalkF"))
			animator.SetTrigger (WebConfig.animator_state_walk);
	}

	private void playerRotateRight(int ratio){
		player.Rotate (new Vector3(0,Time.deltaTime*rotateSpeed*ratio,0));
	}


}
