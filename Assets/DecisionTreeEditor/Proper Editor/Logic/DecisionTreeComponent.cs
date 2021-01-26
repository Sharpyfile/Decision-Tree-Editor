using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DecisionTreeComponent : MonoBehaviour
{
    private DecisionState currentState = null;
    private List<DecisionTreeConnection> connectionsList = new List<DecisionTreeConnection>();
    private List<DecisionState> decisionStates = new List<DecisionState>();
    public List<Trait> traits = new List<Trait>();
    public DecisionTreePrefab decisionTreePrefab;
    private void Start()
    {       
        DecisionState tempState1 = ScriptableObject.CreateInstance(Type.GetType(decisionTreePrefab.nodeContainers[decisionTreePrefab.originalNodeIndex].classType)) as DecisionState;
        tempState1.DecisionTree = GetComponent<DecisionTreeComponent>();
        decisionStates.Add(tempState1);
        currentState = tempState1;

        for(int i = 1; i < decisionTreePrefab.nodeContainers.Count; i++)
        {
            DecisionState tempState2 = ScriptableObject.CreateInstance(Type.GetType(decisionTreePrefab.nodeContainers[i].classType)) as DecisionState;
            tempState2.DecisionTree  = this;
            decisionStates.Add(tempState2);
        }

        // Do przepisania
        for(int i = 0; i < decisionStates.Count; i++)
        {
            List<ConnectionContainer> tempConnections = new List<ConnectionContainer>();
            tempConnections = decisionTreePrefab.connectionContainers.FindAll(x => x.previousNodeID == decisionTreePrefab.nodeContainers[i].nodeID);
            for (int j = 0; j < tempConnections.Count; j++)
            {
                DecisionTreeConnection tempDSConnection = new DecisionTreeConnection();
                ConditionValidator.ConvertStringToConditions(tempDSConnection, tempConnections[j]);
                decisionStates[i].stateConnections.Add(tempDSConnection);
                this.connectionsList.Add(tempDSConnection);
                decisionStates[i].stateConnections[j].nextState = decisionStates[decisionTreePrefab.nodeContainers.FindIndex(x => x.nodeID == tempConnections[j].nextNodeID)];
                decisionStates[i].stateConnections[j].connectionTrait = tempConnections[j].connectionTrait;
            }
        }
        foreach(DecisionState state in this.decisionStates)
        {
            state.DecisionStateStart();
        }
    }
    private void Update()
    {
        // Execute code from the current state at least once before swapping
        currentState.DecisionStateUpdate();  
        // Swap current to one of the states based on the connections 
        CheckStateConditions();
        
    }

    private bool CheckStateConditions()
    {
        foreach(DecisionTreeConnection connection in currentState.stateConnections)
        {
            
            if (traits.Contains(connection.connectionTrait) || connection.connectionTrait == null)
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

    public void SetString(string conditionName, List<string> traits)
    {
        for(int j= 0; j < connectionsList.Count; j++)
        {
            for(int i = 0; i < connectionsList[j].StringBasedConditions.Count; i++)
            {
            
                if (connectionsList[j].StringBasedConditions[i].conditionName == conditionName)
                {
                    if (traits.Contains(connectionsList[j].StringBasedConditions[i].variable2))
                        {
                            connectionsList[j].StringBasedConditions[i]  = new StringBasedCondition(connectionsList[j].StringBasedConditions[i].conditionName, 
                            connectionsList[j].StringBasedConditions[i].operation, connectionsList[j].StringBasedConditions[i].variable2, connectionsList[j].StringBasedConditions[i].variable2);
                        }
                    

                }
            
            }
        }
    }  



    public void StartTreeFromBeggining()
    {
        this.currentState = decisionStates[0];
    }    

}
