using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : BehaviourState
{
    float testing2 = 0;
    public override void BehaviourStateUpdate()
    {
        Debug.Log("Player move");
        Debug.Log(testing2);
        this.BehaviourTree.SetFloat("testing", testing2);
        testing2 += 2.0f;
    }
}
