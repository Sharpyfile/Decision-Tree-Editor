using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DecisionTreeComponent : MonoBehaviour
{
    private BehaviourState currentState = null;
    private List<DecisionTreeConnection> connectionsList = new List<DecisionTreeConnection>();
    private List<BehaviourState> behaviourStates = new List<BehaviourState>();
    public List<Trait> traits = new List<Trait>();
    public DecisionTreePrefab decisionTreePrefab;
    private void Start()
    {       
        BehaviourState tempState1 = (BehaviourState)Activator.CreateInstance(Type.GetType(decisionTreePrefab.nodes[decisionTreePrefab.originalNodeIndex].classType));
        tempState1.BehaviourTree = this;
        behaviourStates.Add(tempState1);
        currentState = tempState1;

        for(int i = 1; i < decisionTreePrefab.nodes.Count; i++)
        {
            BehaviourState tempState2 = Activator.CreateInstance(Type.GetType(decisionTreePrefab.nodes[i].classType)) as BehaviourState;
            tempState2.BehaviourTree  = this;
            behaviourStates.Add(tempState2);
        }

        foreach(BehaviourState state in this.behaviourStates)
        {
            Debug.Log(state.GetType().ToString());
        }


        for(int i = 0; i < behaviourStates.Count; i++)
        {
            List<Connection> tempConnections = new List<Connection>();
            tempConnections = decisionTreePrefab.connections.FindAll(x => x.outPoint == decisionTreePrefab.nodes[i].outPoint);
            for (int j = 0; j < tempConnections.Count; j++)
            {
                DecisionTreeConnection tempDSConnection = new DecisionTreeConnection();
                ConditionValidator.ConvertStringToConditions(tempDSConnection, tempConnections[j]);
                behaviourStates[i].stateConnections.Add(tempDSConnection);
                this.connectionsList.Add(tempDSConnection);
                behaviourStates[i].stateConnections[j].nextState = behaviourStates[decisionTreePrefab.nodes.FindIndex(x => x.nodeID == tempConnections[j].nextNodeID)];
                behaviourStates[i].stateConnections[j].connectionTrait = tempConnections[j].connectionTrait;
                Debug.Log(behaviourStates[i].stateConnections[j].nextState.GetType().ToString());
                Debug.Log(behaviourStates[i].stateConnections[j].connectionTrait.traitName);
            }
        }
    }
    private void Update()
    {
        // Swap current to one of the states based on the connections
        if (CheckStateConditions())
            Debug.Log("Swapped state");
        currentState.BehaviourStateUpdate();   
    }

    private bool CheckStateConditions()
    {

        
        foreach(DecisionTreeConnection connection in currentState.stateConnections)
        {
            if (traits.Contains(connection.connectionTrait))
            {
                if(connection.CheckStateConditions())
                {
                    currentState = connection.nextState;
                    return true;
                }
            }
            
        }
        return false;
    }

    public void SetFloat(string conditionName, float value)
    {
        for(int j= 0; j < connectionsList.Count; j++)
        {
            for(int i = 0; i < connectionsList[j].FloatBasedConditions.Count; i++)
            {
            
                if (connectionsList[j].FloatBasedConditions[i].conditionName == conditionName)
                {
                    connectionsList[j].FloatBasedConditions[i]  = new FloatBasedCondition(connectionsList[j].FloatBasedConditions[i].conditionName, 
                    connectionsList[j].FloatBasedConditions[i].operation, value, connectionsList[j].FloatBasedConditions[i].variable2);

                }
            
            }
        }
    }

    public void SetInt(string conditionName, int value)
    {
        for(int j= 0; j < connectionsList.Count; j++)
        {
            for(int i = 0; i < connectionsList[j].IntBasedConditions.Count; i++)
            {
                if (connectionsList[j].IntBasedConditions[i].conditionName == conditionName)
                {
                    connectionsList[j].IntBasedConditions[i]  = new IntBasedCondition(connectionsList[j].IntBasedConditions[i].conditionName, 
                    connectionsList[j].IntBasedConditions[i].operation, value, connectionsList[j].IntBasedConditions[i].variable2);

                }
            
            }
        }
    }

    public void SetBool(string conditionName, bool value)
    {
        for(int j= 0; j < connectionsList.Count; j++)
        {
            for(int i = 0; i < connectionsList[j].BoolBasedConditions.Count; i++)
            {
            
                if (connectionsList[j].BoolBasedConditions[i].conditionName == conditionName)
                {
                    connectionsList[j].BoolBasedConditions[i]  = new BoolBasedCondition(connectionsList[j].BoolBasedConditions[i].conditionName, 
                    connectionsList[j].BoolBasedConditions[i].operation, value, connectionsList[j].BoolBasedConditions[i].variable2);

                }
            
            }
        }
    }
    public void SetString(string conditionName, string value)
    {
        for(int j= 0; j < connectionsList.Count; j++)
        {
            for(int i = 0; i < connectionsList[j].StringBasedConditions.Count; i++)
            {
            
                if (connectionsList[j].StringBasedConditions[i].conditionName == conditionName)
                {
                    connectionsList[j].StringBasedConditions[i]  = new StringBasedCondition(connectionsList[j].StringBasedConditions[i].conditionName, 
                    connectionsList[j].StringBasedConditions[i].operation, value, connectionsList[j].StringBasedConditions[i].variable2);

                }
            
            }
        }
    }  

    

}
