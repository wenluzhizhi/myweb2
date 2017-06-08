using UnityEngine;
using System.Collections;

public class GenerateAI : MonoBehaviour
{
	public AIFactory aiFactory;
	void Start(){
		aiFactory = new AIFactory ();
		GenerateTank ();
	}


	private void GenerateTank(){

	     aiFactory.getAIEnemy ("tank");

	}

}
