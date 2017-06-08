using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AIFactory:AbstractAIFactory
{
	public override  AIEnemy getAIEnemy (string name)
	{
		if (name == "tank")
		{
			if(NetworkManager.singleton!=null)
			{
				GameObject _tank = NetworkManager.singleton.spawnPrefabs [3];
				if (_tank!= null)
				{				 
					GameObject go=GameObject.Instantiate (_tank) as GameObject;
					return go.GetComponent<AITank> ();
				}
			}	
			return null;
		}
		else
			return null;
	}



		
}



	
