using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {



	void OnCollisionEnter(Collision c){

		if (c.collider.CompareTag ("player"))
		{

			c.collider.SendMessage ("TakeDamage",10);
			Destroy (gameObject);
		}
	}
}
