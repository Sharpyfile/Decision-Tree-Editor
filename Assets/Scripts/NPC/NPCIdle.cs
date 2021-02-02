using UnityEngine;
using UnityEngine.UI;
public class NPCIdle : DecisionState
{
    private Material material;
    
    public override void DecisionStateStart()
    {
        material = this.DecisionTree.transform.Find("NPCModel").GetComponent<Renderer>().material;
    }

    public override void DecisionStateUpdate()
    {
        material.SetColor("_Fresnel_Color", Color.black);
    }

    
}
