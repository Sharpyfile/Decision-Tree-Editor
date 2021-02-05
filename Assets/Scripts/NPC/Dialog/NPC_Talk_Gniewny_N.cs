using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk_Gniewny_N : DecisionState
{private string textString = "Get out of my sight now!";
    private TextMesh characterText;
    private TextMesh characterTraits;
    private Rigidbody rigidbody;
    private Material material;

    // Start is called before the first frame update
    public override void DecisionStateStart()
    {
        foreach(TextMesh textMesh in this.DecisionTree.GetComponentsInChildren<TextMesh>())
        {
            if (textMesh.name == "NPC_Dialog")
                characterText = textMesh;
            else if (textMesh.name == "NPC_Traits")
                characterTraits = textMesh;
        }

        rigidbody = this.DecisionTree.GetComponent<Rigidbody>();
        material = this.DecisionTree.transform.Find("NPCModel").GetComponent<Renderer>().material;
    }
    public override void DecisionStateUpdate()
    {
        material.SetColor("_Fresnel_Color", Color.green);
        if (Input.GetKey(KeyCode.Space))
            characterText.text = textString;

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);       

        string traits = "";
        foreach(string trait in this.DecisionTree.GetComponent<NPCTraits>().traits)  
        {
            traits += trait + "\n";
        }
        characterTraits.text = traits;
        
    }
}
