using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BehaviourState
{
    int test = 0;
    public override void BehaviourStateUpdate()
    {
        // Debug.Log("Player Idle");
        Debug.Log(test);
        this.BehaviourTree.SetInt("test", test);
        test++;
    }
}
