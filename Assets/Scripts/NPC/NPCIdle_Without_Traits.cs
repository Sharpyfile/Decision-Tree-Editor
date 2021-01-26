using UnityEngine;
using UnityEngine.UI;
public class NPCIdle_Without_Traits : DecisionState
{

    
    public override void DecisionStateStart()
    {
        this.DecisionTree.SetString("trait", this.DecisionTree.GetComponent<NPCTraits>().traits);
    }

    public override void DecisionStateUpdate()
    {
        this.DecisionTree.SetString("trait", this.DecisionTree.GetComponent<NPCTraits>().traits);
    }

    
}
