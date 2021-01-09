using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeComponent : MonoBehaviour
{
    private BehaviourState currentState = null;

    private PlayerIdle playerIdle;
    private PlayerMove playerMove;
    private PlayerRun playerRun;

    private Dictionary<string, BehaviourStateConnection> connectionDictiorary;

    private void Start()
    {
        connectionDictiorary = new Dictionary<string, BehaviourStateConnection>();
        playerIdle = new PlayerIdle();
        playerMove = new PlayerMove();
        playerRun = new PlayerRun();

        IntBasedCondition test1 = new IntBasedCondition("test1", Operation.IsGreater, 0, 100);
        IntBasedCondition test2 = new IntBasedCondition("test2", Operation.IsGreater, 0, 50);
        

        BehaviourStateConnection testConnection1 = new BehaviourStateConnection(playerMove);
        testConnection1.IntBasedConditions.Add(test1.conditionName, test1);
        BehaviourStateConnection testConnection2 = new BehaviourStateConnection(playerRun);
        testConnection2.IntBasedConditions.Add(test2.conditionName, test2);

        connectionDictiorary.Add("testConnection1", testConnection1);
        connectionDictiorary.Add("testConnection2", testConnection1);

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
        foreach(BehaviourStateConnection connection in connectionDictiorary.Values)
        {
            if (connection.FloatBasedConditions.ContainsKey(conditionName))
            {
                FloatBasedCondition temp = connection.FloatBasedConditions[conditionName];
                temp.variable1 = value;
                connection.FloatBasedConditions[conditionName] = temp;
                return;
            }
        }
    }

    public void SetInt(string conditionName, int value)
    {
        foreach(BehaviourStateConnection connection in connectionDictiorary.Values)
        {
            if (connection.IntBasedConditions.ContainsKey(conditionName))
            {
                IntBasedCondition temp = connection.IntBasedConditions[conditionName];
                temp.variable1 = value;
                connection.IntBasedConditions[conditionName] = temp;
                return;
            }
        }
    }

    public void SetBool(string conditionName, bool value)
    {
        foreach(BehaviourStateConnection connection in connectionDictiorary.Values)
        {
            if (connection.BoolBasedConditions.ContainsKey(conditionName))
            {
                BoolBasedCondition temp = connection.BoolBasedConditions[conditionName];
                temp.variable1 = value;
                connection.BoolBasedConditions[conditionName] = temp;
                return;
            }
        }
    }

    public void SetString(string conditionName, string value)
    {
        foreach(BehaviourStateConnection connection in connectionDictiorary.Values)
        {
            if (connection.StringBasedConditions.ContainsKey(conditionName))
            {
                StringBasedCondition temp = connection.StringBasedConditions[conditionName];
                temp.variable1 = value;
                connection.StringBasedConditions[conditionName] = temp;
                return;
            }
        }
    }  

}
