using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BehaviourState
{
    int test = 0;
    int test2 = 0;
    public override void BehaviourStateUpdate()
    {
        // Debug.Log("Player Idle");
        Debug.Log(test);
        if (Input.GetKey(KeyCode.Space))
        {
            this.BehaviourTree.SetInt("test2", test2);
            test2 += 10;
        }
            
        this.BehaviourTree.SetInt("test", test);
        test++;
    }
}
