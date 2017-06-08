using UnityEngine;
using System.Collections;

public abstract class AbstractAIFactory
{
	public abstract AIEnemy getAIEnemy(string name);
}
