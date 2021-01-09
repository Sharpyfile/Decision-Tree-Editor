using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BehaviourStateConnection
{
    public BehaviourStateConnection(BehaviourState nextState)
    {
        this.nextState = nextState;
    }
    public BehaviourState nextState = null;
    public Dictionary<string ,IntBasedCondition> IntBasedConditions = new Dictionary<string, IntBasedCondition>();
    public Dictionary<string ,FloatBasedCondition> FloatBasedConditions = new Dictionary<string, FloatBasedCondition>();
    public Dictionary<string ,BoolBasedCondition> BoolBasedConditions = new Dictionary<string, BoolBasedCondition>();
    public Dictionary<string ,StringBasedCondition> StringBasedConditions = new Dictionary<string, StringBasedCondition>();

    public bool CheckStateConditions()
    {
        return ConditionValidator.CheckConnectionConditions(this);
    }
}
