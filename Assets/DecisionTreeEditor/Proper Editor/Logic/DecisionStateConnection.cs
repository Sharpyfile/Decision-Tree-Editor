using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionTreeConnection
{

    public DecisionTreeConnection()
    {
        this.IntBasedConditions = new List<IntBasedCondition>();
        this.FloatBasedConditions = new List<FloatBasedCondition>();
        this.BoolBasedConditions = new List<BoolBasedCondition>();
        this.StringBasedConditions = new List<StringBasedCondition>();
    }

    public BehaviourState nextState = null;
    public List<IntBasedCondition> IntBasedConditions; 
    public List<FloatBasedCondition> FloatBasedConditions; 
    public List<BoolBasedCondition> BoolBasedConditions; 
    public List<StringBasedCondition> StringBasedConditions; 
    public Trait connectionTrait = null;

    public bool CheckStateConditions()
    {
        
        return ConditionValidator.CheckConnectionConditions(this);
    }

    public override string ToString()
    {
        string toString = "";
        toString += "IntBasedCondition\n";
        foreach(IntBasedCondition con in IntBasedConditions)
        {
            toString += con.conditionName + " " + con.operation.ToString() + " " + con.variable1.ToString() + " " + con.variable2.ToString() + "\n";
        }
        toString += "FloatBasedCondition\n";
        foreach(FloatBasedCondition con in FloatBasedConditions)
        {
            toString += con.conditionName + " " + con.operation.ToString() + " " + con.variable1.ToString() + " " + con.variable2.ToString() + "\n";
        }
        toString += "BoolBasedCondition\n";
        foreach(BoolBasedCondition con in BoolBasedConditions)
        {
            toString += con.conditionName + " " + con.operation.ToString() + " " + con.variable1.ToString() + " " + con.variable2.ToString() + "\n";
        }
        toString += "StringBasedCondition\n";
        foreach(StringBasedCondition con in StringBasedConditions)
        {
            toString += con.conditionName + " " + con.operation.ToString() + " " + con.variable1.ToString() + " " + con.variable2.ToString() + "\n";
        }
        return base.ToString();
    }
}
