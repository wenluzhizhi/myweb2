using UnityEngine;
using System.Collections;

public abstract class  BaseAIPlayer: MonoBehaviour 
{
	private Animator animator;
	private string string_idle = "idle";
	private string string_walk = "walk";
	private string string_attack="attack";
	private string string_jump="jump";

	void Start(){
		animator = this.gameObject.GetComponent<Animator> ();
	}
	public virtual void Animator_walk(){
		
	}

	public virtual void Animator_attack(){

	}

	public virtual void Animator_idle(){

	}

	public virtual void Animator_jump(){

	}
}
