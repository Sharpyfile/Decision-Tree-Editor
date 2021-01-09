using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourState
{
    //Object that will be added directly to the scene
    public GameObject parentObject = null;

    public List<BehaviourStateConnection> stateConnections;

    public BehaviourState()
    {
        stateConnections = new List<BehaviourStateConnection>();    
    }   

    //The equivalent of Update in MonoBehaviour. Override it throught the script 
    public virtual void BehaviourStateUpdate()
    {
        Debug.Log("I live");
    }


    
}
