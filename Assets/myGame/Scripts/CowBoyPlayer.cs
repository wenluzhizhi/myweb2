using UnityEngine;
using System.Collections;

public class CowBoyPlayer : MonoBehaviour
{

	public Transform Player;
	public Animator animator;
	private BoxCollider boxCollider;
	private Rigidbody rigidbody;
	public int rotateSpeed=20;
	public int thrustSpeed=20;

	private string animator_state_walk ="walk";
	private string animator_state_idle ="idle";
	private string animator_state_attack ="attack";

	private AnimatorStateInfo animatorState = new AnimatorStateInfo();
	void Start () {
		if (Player == null)
			this.Player = this.gameObject.GetComponent<Transform> ();
		if (animator == null)
			animator = this.gameObject.GetComponent<Animator> ();
		this.boxCollider = this.gameObject.GetComponent<BoxCollider> ();
		this.rigidbody = this.gameObject.GetComponent<Rigidbody> ();
		boxCollider.center = new Vector3 (0,1,0);


	}
	void Update () {	   
		animatorState = animator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetKey (KeyCode.UpArrow)) {
			playerforward (1);

			if (Input.GetKey (KeyCode.Space)) {
				//Debug.Log ("trigger the same tiem:"+Time.time);
				this.rigidbody.AddForce(transform.forward*thrustSpeed);
			}



		} else if (Input.GetKey (KeyCode.DownArrow)) {
			playerforward (-1);
		} 
		else 
		{
			if (!animatorState.IsName ("DualIdle")&&!animatorState.IsName ("2HAttack")) {
				animator.SetTrigger (animator_state_idle);
			}
			  
		}

		if (Input.GetKey (KeyCode.J)) {
			if(!animatorState.IsName("2HAttack"))
				animator.SetTrigger (animator_state_attack);
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

	private void playerforward(int ratio){
		Player.Translate (Vector3.forward * Time.deltaTime*ratio);
		if(!animatorState.IsName("1HWalkF"))
		   animator.SetTrigger (animator_state_walk);
	}
	private void playerRotateRight(int ratio){
		Player.Rotate (new Vector3(0,Time.deltaTime*rotateSpeed*ratio,0));

	}

}
