using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health :NetworkBehaviour
{


	public const int maxHealth=100;

	[SyncVar(hook="OnChangeHealth")]
	public int currentHealth=maxHealth;

	public Slider slider;
	public bool destroyOnDeath = false;


	private NetworkStartPosition[] spawnPoints;


	public void TakeDamage(int damage){

		if (!isServer)
			return;
		currentHealth -= damage;
		if (currentHealth <= 0) {

			if (destroyOnDeath) {

				Destroy (gameObject);
				return;
			}
			currentHealth = maxHealth;
			RpcRespawn ();
		}
			
		
	}

	[ClientRpc]
	void RpcRespawn(){

		if(isLocalPlayer)
		   transform.position = Vector3.zero;
	}


	public void OnChangeHealth(int health){
		slider.value = (float)health / maxHealth;
	}
}
