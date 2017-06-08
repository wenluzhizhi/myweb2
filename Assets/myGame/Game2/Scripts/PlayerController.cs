using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
	void Start(){
		player = this.gameObject.GetComponent<Transform> ();
		animator = this.gameObject.GetComponent<Animator> ();
		rigidbody = this.gameObject.GetComponent<Rigidbody> ();
        if (isLocalPlayer)
        {
            this.gameObject.GetComponent<CameraFollow>().enabled = true;
        }
        else {
            this.gameObject.GetComponent<CameraFollow>().enabled = false;
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
            CmdshootBullet();
        }
        else
        {
            StopAttack();
        }
	}

    [Command]
    public void CmdshootBullet()
    {
        GameObject _bullet = GameObject.Instantiate(bulletPrefab, player.transform.position + player.forward * 0.1f + player.up * 1.5f, Quaternion.identity) as GameObject;
        _bullet.gameObject.GetComponent<Rigidbody>().velocity = player.forward * 30;
        Destroy(_bullet, 2);
        NetworkServer.Spawn(_bullet);
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
		player.Translate (Vector3.forward*Time.deltaTime*ratio);
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
		player.Rotate (new Vector3(0,Time.deltaTime*rotateSpeed*ratio,0));
	}


}
