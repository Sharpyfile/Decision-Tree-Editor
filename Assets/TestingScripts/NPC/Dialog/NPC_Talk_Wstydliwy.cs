using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk_Wstydliwy : DecisionState
{private string textString = "I would rather not talk...";
    private TextMesh characterText;
    private TextMesh characterTraits;
    private Rigidbody rigidbody;

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
        
    }
    public override void DecisionStateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
            characterText.text = textString;

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);       

        string traits = "";
        foreach(Trait trait in this.DecisionTree.traits)  
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        
    }
}
