using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : DecisionState
{
    int test = 0;
    int test2 = 0;
    public override void DecisionStateStart()
    {
        
    }
    public override void DecisionStateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.DecisionTree.SetInt("test2", test2);
            test2 += 10;
        }
            
        this.DecisionTree.SetInt("test", test);
        test++;
    }
}
