using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    String,
    Trait
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

    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
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

    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
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

    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
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

    public override string ToString() => $"{conditionName},{operation},{variable1},{variable2}";
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

    public static void ConvertStringToConditions(DecisionTreeConnection connection, Connection editorConnection)
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

    // Used to convert loaded conditions into proper ones
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


