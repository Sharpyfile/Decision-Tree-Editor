using UnityEngine;
using UnityEngine.UI;

public class NPC_Movement_End : DecisionState
{
    private string textString = "";
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
        characterText.text = textString;

        string traits = "";
        characterTraits.text = traits;
        this.DecisionTree.SetFloat("dragonDistance", 0);  
        
        this.DecisionTree.StartTreeFromBeggining();

    }
}
