using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviourTreeComponent : MonoBehaviour
{
    private BehaviourState currentState = null;
    private List<BehaviourStateConnection> connectionsList = new List<BehaviourStateConnection>();
    private List<BehaviourState> behaviourStates = new List<BehaviourState>();
    public List<Trait> traits = new List<Trait>();
    public BehaviourTreePrefab behaviourTreePrefab;
    private void Start()
    {       
        BehaviourState tempState1 = (BehaviourState)Activator.CreateInstance(Type.GetType(behaviourTreePrefab.nodes[behaviourTreePrefab.originalNodeIndex].classType));
        tempState1.BehaviourTree = this;
        behaviourStates.Add(tempState1);
        currentState = tempState1;

        for(int i = 1; i < behaviourTreePrefab.nodes.Count; i++)
        {
            BehaviourState tempState2 = Activator.CreateInstance(Type.GetType(behaviourTreePrefab.nodes[i].classType)) as BehaviourState;
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
            tempConnections = behaviourTreePrefab.connections.FindAll(x => x.outPoint == behaviourTreePrefab.nodes[i].outPoint);
            for (int j = 0; j < tempConnections.Count; j++)
            {
                BehaviourStateConnection tempBSConnection = new BehaviourStateConnection();
                ConvertStringToConditions(tempBSConnection, tempConnections[j]);
                behaviourStates[i].stateConnections.Add(tempBSConnection);
                this.connectionsList.Add(tempBSConnection);
                behaviourStates[i].stateConnections[j].nextState = behaviourStates[behaviourTreePrefab.nodes.FindIndex(x => x.nodeID == tempConnections[j].nextNodeID)];
                Debug.Log(behaviourStates[i].stateConnections[j].nextState.GetType().ToString());
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
        foreach(BehaviourStateConnection connection in currentState.stateConnections)
        {
            if(connection.CheckStateConditions())
            {
                currentState = connection.nextState;
                return true;
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
                    return;
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
                    return;
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
                    return;
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
                    return;
                }
            
            }
        }
    }  

    private void ConvertStringToConditions(BehaviourStateConnection connection, Connection editorConnection)
    {
        foreach(string condition in editorConnection.intBasedConditionsToString)
        {
            IntBasedCondition newCondition = new IntBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = int.Parse(parts[2]);
            newCondition.variable2 = Int16.Parse(parts[3]);
            connection.IntBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.floatBasedConditionsToString)
        {
            FloatBasedCondition newCondition = new FloatBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = float.Parse(parts[2]);
            newCondition.variable2 = float.Parse(parts[3]);
            connection.FloatBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.boolBasedConditionsToString)
        {
            BoolBasedCondition newCondition = new BoolBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = bool.Parse(parts[2]);
            newCondition.variable2 = bool.Parse(parts[3]);
            connection.BoolBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.stringBasedConditionsToString)
        {
            StringBasedCondition newCondition = new StringBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = parts[2];
            newCondition.variable2 = parts[3];
            connection.StringBasedConditions.Add(newCondition);
        }
    }

}
