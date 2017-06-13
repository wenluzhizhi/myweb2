using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace MyGame.Two{


public class PlayerController:NetworkBehaviour{


	public Transform player;
	private Animator animator;
	private BoxCollider boxcollider;
	private Rigidbody rigidbody;

	private AnimatorStateInfo animatorState = new AnimatorStateInfo ();
	public int rotateSpeed=20;
	public int thrustSpeed=20;

    [SerializeField]
    private GameObject bulletPrefab;
	[SerializeField]
	private Transform shootBulletPos;


		private BoxCollider playerBoxcollider;

	[SyncVar(hook="OnScoreChange")]
	public int PlayerScore;

	public override void OnStartLocalPlayer ()
	{
		player = this.gameObject.GetComponent<Transform> ();
		animator = this.gameObject.GetComponent<Animator> ();
		rigidbody = this.gameObject.GetComponent<Rigidbody> ();
			playerBoxcollider = this.gameObject.GetComponent<BoxCollider> ();
			playerBoxcollider.center = new Vector3 (0,0.7f,0);
		if (isLocalPlayer)
		{
			this.gameObject.GetComponent<CameraFollow>().enabled = true;

		}
		else 
		{
			this.gameObject.GetComponent<CameraFollow>().enabled = false;

		}
		CmdAddPlayerToServer ();
	}



	[Command]
	public void CmdAddPlayerToServer()
	{
		if (SoftSetUp.Instance != null)
		{
			
			SoftSetUp.Instance.AddPlayer (this);
		} 
		else 
		{
			Debug.Log ("---softSetup...Null"+Time.time);

		}
		   
	}




	void Update(){



		if(!isLocalPlayer) return;
		animatorState = animator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetKey (KeyCode.UpArrow))
        {
			playerForward (1);	
		}
        else if (Input.GetKey (KeyCode.DownArrow))
        {
            playerForward(-1);
		}
        else
        {
			if (animatorState.IsName("1HWalkF"))
            {
                animator.SetBool(WebConfig.animator_state_walk, false);
            }
            animator.SetBool(WebConfig.animator_state_idle, true);
        }

		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerRotateRight (-5);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRotateRight (5);
		}

		if (Input.GetKeyDown (KeyCode.Space))
        {
			if (this.transform.position.y < 5)
            {
				this.rigidbody.velocity = new Vector3 (0,5,0);
                PlayJump();
			}
            
		}
        else
        {
            StopJump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayAttack();
			if (this.netId != null) {
				CmdshootBullet ();
			}

        }
        else
        {
            StopAttack();
        }
	}

	



	[Command]
	public void  CmdshootBullet()
    {
		 GameObject _bullet = GameObject.Instantiate(bulletPrefab,shootBulletPos.position, Quaternion.identity) as GameObject;
		_bullet.gameObject.SetActive (true);
		_bullet.gameObject.GetComponent<PlayerBulletController> ().owner = this;
        _bullet.gameObject.GetComponent<Rigidbody>().velocity = player.forward * 30;
        Destroy(_bullet, 2);
		NetworkServer.Spawn (_bullet);
    }




    public void PlayJump()
    {
        if (!animatorState.IsName("cyclop_jump"))
        {
            animator.SetBool(WebConfig.animator_state_jump, true);
        }
    }

    public void StopJump()
    {
        if (animatorState.IsName("cyclop_jump"))
        {
            animator.SetBool(WebConfig.animator_state_jump, false);
        }
    }


    public void PlayAttack()
    {
        if (!animatorState.IsName("2HAttack"))
        {
            animator.SetBool(WebConfig.animator_state_attack, true);
        }
    }

    public void StopAttack()
    {
        if (animatorState.IsName("2HAttack"))
        {
            animator.SetBool(WebConfig.animator_state_attack,false);
        }
    }





    private void playerForward(int ratio){
		player.Translate (Vector3.forward*Time.deltaTime*ratio*thrustSpeed);
        wald();
        

    }
    public void wald()
    {
        if (!animatorState.IsName("1HWalkF"))
        {
            animator.SetBool(WebConfig.animator_state_walk, true);
            animator.SetBool(WebConfig.animator_state_idle, false);
        }

    }
	private void playerRotateRight(int ratio){
		player.Rotate (new Vector3(0,Time.deltaTime*rotateSpeed*ratio*rotateSpeed,0));
	}




	public void OnScoreChange(int k){
		if (isLocalPlayer) {
			SoftSetUp.Instance.CurrentPlayerScore (k);
		}
	}


	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag==WebConfig.tagName_tank_bullet)
		{
			if (isLocalPlayer) {
				PlayerScore -= 1;	
			}
			Destroy (c.gameObject);
		}
	}


}


}