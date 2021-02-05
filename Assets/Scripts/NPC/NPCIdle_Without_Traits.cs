using UnityEngine;
using UnityEngine.UI;
public class NPCIdle_Without_Traits : DecisionState
{
    private Material material;

    public override void DecisionStateStart()
    {
        material = this.DecisionTree.transform.Find("NPCModel").GetComponent<Renderer>().material;
        this.DecisionTree.SetString("trait", this.DecisionTree.GetComponent<NPCTraits>().traits);
    }

    public override void DecisionStateUpdate()
    {
        material.SetColor("_Fresnel_Color", Color.black);
        this.DecisionTree.SetString("trait", this.DecisionTree.GetComponent<NPCTraits>().traits);
    }

    
}
