using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionState : ScriptableObject
{
    //Object that will be added directly to the scene
    public GameObject parentObject = null;

    public List<DecisionTreeConnection> stateConnections = new List<DecisionTreeConnection>();

    public DecisionTreeComponent DecisionTree = null;


    //The equivalent of Update in MonoBehaviour. Override it throught the script 
    public virtual void DecisionStateStart()
    {

    }
    public virtual void DecisionStateUpdate()
    {

    }


    
}
