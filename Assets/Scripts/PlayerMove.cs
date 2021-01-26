using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : DecisionState
{
    float testing2 = 0;
    public override void DecisionStateStart()
    {
        Debug.Log("AAAA...");
    }
    public override void DecisionStateUpdate()
    {
        this.DecisionTree.SetFloat("testing", testing2);
        testing2 += 2.0f;
    }
}
