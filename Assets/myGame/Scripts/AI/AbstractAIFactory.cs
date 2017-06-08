using UnityEngine;
using System.Collections;

public abstract class AbstractAIFactory{
    public abstract AITank getTank(string color);   
}
