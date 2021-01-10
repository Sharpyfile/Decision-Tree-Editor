using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BehaviourStateConnection
{
    public BehaviourStateConnection(BehaviourState nextState)
    {
        this.nextState = nextState;
    }

    public BehaviourStateConnection(string pathToNextState)
    {
        this.pathToNextState = pathToNextState;
    }

    public BehaviourState nextState = null;

    public string pathToNextState = null;
    public List<IntBasedCondition> IntBasedConditions = new List<IntBasedCondition>();
    public List<FloatBasedCondition> FloatBasedConditions = new List<FloatBasedCondition>();
    public List<BoolBasedCondition> BoolBasedConditions = new List<BoolBasedCondition>();
    public List<StringBasedCondition> StringBasedConditions = new List<StringBasedCondition>();

    public bool CheckStateConditions()
    {
        return ConditionValidator.CheckConnectionConditions(this);
    }
}
