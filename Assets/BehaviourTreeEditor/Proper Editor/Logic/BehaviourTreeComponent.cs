using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeComponent : MonoBehaviour
{
    private BehaviourState currentState = null;

    private PlayerIdle playerIdle;
    private PlayerMove playerMove;
    private PlayerRun playerRun;

    private List<BehaviourStateConnection> connectionsList;

    private void Start()
    {

        // Add loading all assets as Instatiate by BehaviourStateConnectio.pathToNextState
        // Resourses.Load(path) as BehaviourState
        connectionsList = new List<BehaviourStateConnection>();
        playerIdle = new PlayerIdle();
        playerMove = new PlayerMove();
        playerRun = new PlayerRun();

        IntBasedCondition test1 = new IntBasedCondition("test1", Operation.IsGreater, 0, 100);
        IntBasedCondition test2 = new IntBasedCondition("test2", Operation.IsGreater, 0, 50);
        

        BehaviourStateConnection testConnection1 = new BehaviourStateConnection(playerMove);
        testConnection1.IntBasedConditions.Add(test1);
        BehaviourStateConnection testConnection2 = new BehaviourStateConnection(playerRun);
        testConnection2.IntBasedConditions.Add(test2);

        connectionsList.Add(testConnection1);
        connectionsList.Add(testConnection1);

        playerIdle.stateConnections.Add(testConnection1);
        playerIdle.stateConnections.Add(testConnection2);
        

        currentState = playerIdle;
    }
    int testInt = 0;
    private void Update()
    {

        SetInt("test1", testInt);
        testInt++;
        Debug.Log(testInt);
        // Swap current to one of the states based on the connections
        //Debug.Log(connectionDictiorary["testConnection1"].IntBasedConditions["test1"].variable1);
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

}
