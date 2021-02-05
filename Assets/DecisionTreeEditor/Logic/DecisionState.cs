using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionState : ScriptableObject
{
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
