using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Operation
{
    IsGreater,
    IsSmaller,
    IsEqual,
    IsDifferent
}
public struct IntBasedCondition
{
    
    public IntBasedCondition(string conditionName, Operation operation, int variable1, int variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }
    public string conditionName {get; set;}
    public Operation operation {get; set;}
    public int variable1 {get; set;}
    public int variable2 {get; set;}

    public override string ToString() => $"({conditionName}, {operation}, {variable1}, {variable2})";
}

public struct FloatBasedCondition
{
    public FloatBasedCondition(string conditionName, Operation operation, float variable1, float variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }
    public string conditionName {get; set;}
    public Operation operation {get; set;}
    public float variable1 {get; set;}
    public float variable2 {get; set;}

    public override string ToString() => $"({conditionName}, {operation}, {variable1}, {variable2})";
}


public struct BoolBasedCondition
{
    public BoolBasedCondition(string conditionName, Operation operation, bool variable1, bool variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }
    public string conditionName {get; set;}
    public Operation operation {get; set;}
    public bool variable1 {get; set;}
    public bool variable2 {get; set;}

    public override string ToString() => $"({conditionName}, {operation}, {variable1}, {variable2})";
}

public struct StringBasedCondition
{
    public StringBasedCondition(string conditionName, Operation operation, string variable1, string variable2)
    {
        this.conditionName = conditionName;
        this.operation = operation;
        this.variable1 = variable1;
        this.variable2 = variable2;
    }
    public string conditionName {get; set;}
    public Operation operation {get; set;}
    public string variable1 {get; set;}
    public string variable2 {get; set;}

    public override string ToString() => $"({conditionName}, {operation}, {variable1}, {variable2})";
}


public static class ConditionValidator 
{
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

    public static bool CheckConnectionConditions(BehaviourStateConnection connection)
    {
        Debug.Log(connection.IntBasedConditions.Count);
        //Debug.Log(connection.IntBasedConditions.Count);

        foreach(IntBasedCondition condition in connection.IntBasedConditions.Values)
        {
            if(!IntBasedConditionCheck(condition))
                return false;
        }
        foreach(FloatBasedCondition condition in connection.FloatBasedConditions.Values)
        {
            if(!FloatBasedConditionCheck(condition))
                return false;
        }
        foreach(BoolBasedCondition condition in connection.BoolBasedConditions.Values)
        {
            if(!BoolBasedConditionCheck(condition))
                return false;
        }
        foreach(StringBasedCondition condition in connection.StringBasedConditions.Values)
        {
            if(!StringBasedConditionCheck(condition))
                return false;
        }
        return true;
    }
}

