using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ConnectionContainer
{
    /* Variable: intBasedConditions
     * List of the condition with type int
     */
    public List<string> intBasedConditions;

    /* Variable: floatBasedConditions
     * List of the condition with type float
     */
    public List<string> floatBasedConditions;

    /* Variable: boolBasedConditions
     * List of the condition with type bool
     */
    public List<string> boolBasedConditions;

    /* Variable: stringBasedConditions
     * List of the condition with type string
     */
    public List<string> stringBasedConditions;

    /* Variable: connectionTrait
     * Trait for this connection
     */
    public Trait connectionTrait;

    /* Variable: previousNodeID
     * Represents ID of the node that leads to this connection
     */
    public string previousNodeID;

    /* Variable: nextNodeID
     * Represents ID of the node that will be next after all conditions in this
     * connection will be met
     */
    public string nextNodeID;

    /* Function: ConnectionContainer
     * Constructor
     */
    public ConnectionContainer()
    {
        intBasedConditions = new List<string>();
        floatBasedConditions = new List<string>();
        boolBasedConditions = new List<string>();
        stringBasedConditions = new List<string>();
    }
    
}

[Serializable]
public struct NodeContainer
{
    /* Variable: nodeID
     * Index in the list converted to string
     */
    public string nodeID;

    /* Variable: classType
     * Name of the classType used for creating instances
     */
    public string classType;
}

public enum Operation
{
    IsGreater,
    IsSmaller,
    IsEqual,
    IsDifferent
}

public enum TypeOfCondition
{
    Int,
    Float,
    Bool,
    String
}
public struct IntBasedCondition
{

    /* Function: IntBasedCondition
     * Constructor, takes name of the condition, operation, starting variable (0) and variable to compare to
     */
    public IntBasedCondition(string conditionName, Operation operation, int variable1, int variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }

    /* Variable: conditionName
     * Name of the condition
     */
    public string conditionName {get; set;}

    /* Variable: operation
     * Operation between two variables
     */
    public Operation operation {get; set;}

    /* Variable: variable1
     * Starting variable
     */
    public int variable1 {get; set;}

    /* Variable: variable2
     * Variable to compare
     */
    public int variable2 {get; set;}

    /* Function: ToString
     * Converts Class to string
     */
    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
}

public struct FloatBasedCondition
{
    /* Function: FloatBasedCondition
     * Constructor, takes name of the condition, operation, starting variable (0.0f) and variable to compare to
     */
    public FloatBasedCondition(string conditionName, Operation operation, float variable1, float variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }

    /* Variable: conditionName
     * Name of the condition
     */
    public string conditionName {get; set;}

    /* Variable: operation
     * Operation between two variables
     */
    public Operation operation {get; set;}

    /* Variable: variable1
     * Starting variable
     */
    public float variable1 {get; set;}

    /* Variable: variable2
     * Variable to compare
     */
    public float variable2 {get; set;}

    /* Function: ToString
     * Converts Class to string
     */
    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
}


public struct BoolBasedCondition
{
    /* Function: BoolBasedCondition
     * Constructor, takes name of the condition, operation, starting variable (false) and variable to compare to
     */
    public BoolBasedCondition(string conditionName, Operation operation, bool variable1, bool variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }

    /* Variable: conditionName
     * Name of the condition
     */
    public string conditionName {get; set;}

    /* Variable: operation
     * Operation between two variables
     */
    public Operation operation {get; set;}

    /* Variable: variable1
     * Starting variable
     */
    public bool variable1 {get; set;}

    /* Variable: variable2
     * Variable to compare
     */
    public bool variable2 {get; set;}

    /* Function: ToString
     * Converts Class to string
     */
    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
}

public struct StringBasedCondition
{

    /* Function: StringBasedCondition
     * Constructor, takes name of the condition, operation, starting variable ("") and variable to compare to
     */
    public StringBasedCondition(string conditionName, Operation operation, string variable1, string variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }

    /* Variable: conditionName
     * Name of the condition
     */
    public string conditionName {get; set;}

    /* Variable: operation
     * Operation between two variables
     */
    public Operation operation {get; set;}

    /* Variable: variable1
     * Starting variable
     */
    public string variable1 {get; set;}

    /* Variable: variable2
     * Variable to compare
     */
    public string variable2 {get; set;}

    /* Function: ToString
     * Converts Class to string
     */
    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
}


public static class ConditionValidator 
{
    /* Function: IntBasedConditionCheck
     * Takes IntBasedCondition and checks returns true if condition met
     * else it returns false
     */
    static bool IntBasedConditionCheck(IntBasedCondition condition)
    {
        switch(condition.operation)
        {
            case Operation.IsDifferent:
                return (condition.variable1 != condition.variable2);
            
            case Operation.IsEqual:
                return (condition.variable1 == condition.variable2);

            case Operation.IsGreater:
                return (condition.variable1 > condition.variable2);
            
            case Operation.IsSmaller:
                return (condition.variable1 < condition.variable2);

            default:
                return false;
        }
    }

    /* Function: FloatBasedConditionCheck
     * Takes FloatBasedCondition and checks returns true if condition met
     * else it returns false
     */
    static bool FloatBasedConditionCheck(FloatBasedCondition condition)
    {
        switch(condition.operation)
        {
            case Operation.IsGreater:
                return (condition.variable1 > condition.variable2);
            
            case Operation.IsSmaller:
                return (condition.variable1 < condition.variable2);

            default:
                return false;
        }
    }

    /* Function: BoolBasedConditionCheck
     * Takes BoolBasedCondition and checks returns true if condition met
     * else it returns false
     */
    static bool BoolBasedConditionCheck(BoolBasedCondition condition)
    {
        switch(condition.operation)
        {
            case Operation.IsDifferent:
                return (condition.variable1 != condition.variable2);
            
            case Operation.IsEqual:
                return (condition.variable1 == condition.variable2);

            default:
                return false;
        }
    }

    /* Function: StringBasedConditionCheck
     * Takes StringBasedCondition and checks returns true if condition met
     * else it returns false
     */
    static bool StringBasedConditionCheck(StringBasedCondition condition)
    {
        switch(condition.operation)
        {
            case Operation.IsDifferent:
                return (condition.variable1 != condition.variable2);
            
            case Operation.IsEqual:
                return (condition.variable1 == condition.variable2);

            default:
                return false;
        }
    }

    /* Function: CheckConnectionConditions
     * Takes DecisionTreeConnection and checks all conditions
     * Returns true if all are met, else returns false
     */
    public static bool CheckConnectionConditions(DecisionTreeConnection connection)
    {
        foreach(IntBasedCondition condition in connection.IntBasedConditions)
        {
            if(!IntBasedConditionCheck(condition))
                return false;
        }
        foreach(FloatBasedCondition condition in connection.FloatBasedConditions)
        {
            if(!FloatBasedConditionCheck(condition))
                return false;
        }
        foreach(BoolBasedCondition condition in connection.BoolBasedConditions)
        {
            if(!BoolBasedConditionCheck(condition))
                return false;
        }
        foreach(StringBasedCondition condition in connection.StringBasedConditions)
        {
            if(!StringBasedConditionCheck(condition))
                return false;
        }
        return true;
    }

    /* Function: ConvertStringToConditions
     * Takes DecisionTreeConnection and ConnectionContainer and
     * converts all conditions in string from ConnectionContainer to
     * proper conditions in DecisionTreeConnection
     */
    public static void ConvertStringToConditions(DecisionTreeConnection connection, ConnectionContainer editorConnection)
    {
        foreach(string condition in editorConnection.intBasedConditions)
        {
            IntBasedCondition newCondition = new IntBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = int.Parse(parts[2]);
            newCondition.variable2 = Int16.Parse(parts[3]);
            connection.IntBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.floatBasedConditions)
        {
            FloatBasedCondition newCondition = new FloatBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = float.Parse(parts[2]);
            newCondition.variable2 = float.Parse(parts[3]);
            connection.FloatBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.boolBasedConditions)
        {
            BoolBasedCondition newCondition = new BoolBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = bool.Parse(parts[2]);
            newCondition.variable2 = bool.Parse(parts[3]);
            connection.BoolBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.stringBasedConditions)
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

    /* Function: ConvertStringToConditions
     * Takes Connection and Connection and
     * converts all conditions in string from second Connection to
     * proper conditions in first Connection
     */
    public static void ConvertStringToConditions(Connection newConnection, Connection editorConnection)
    {
        foreach(string condition in editorConnection.intBasedConditionsToString)
        {
            IntBasedCondition newCondition = new IntBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = int.Parse(parts[2]);
            newCondition.variable2 = Int16.Parse(parts[3]);
            newConnection.intBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.floatBasedConditionsToString)
        {
            FloatBasedCondition newCondition = new FloatBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = float.Parse(parts[2]);
            newCondition.variable2 = float.Parse(parts[3]);
            newConnection.floatBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.boolBasedConditionsToString)
        {
            BoolBasedCondition newCondition = new BoolBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = bool.Parse(parts[2]);
            newCondition.variable2 = bool.Parse(parts[3]);
            newConnection.boolBasedConditions.Add(newCondition);
        }

        foreach(string condition in editorConnection.stringBasedConditionsToString)
        {
            StringBasedCondition newCondition = new StringBasedCondition();
            string[] parts = condition.Split(',');
            newCondition.conditionName = parts[0];
            newCondition.operation = (Operation)Enum.Parse(typeof(Operation), parts[1]);
            newCondition.variable1 = parts[2];
            newCondition.variable2 = parts[3];
            newConnection.stringBasedConditions.Add(newCondition);
        }
    }

    
}


