using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk_End : DecisionState
{
    private string textString = "";
    private Text characterText;
    private Text characterTraits;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    public override void DecisionStateStart()
    {
        characterText = GameObject.Find("CharacterText").GetComponent<Text>();
        characterTraits = GameObject.Find("CharacterTraits").GetComponent<Text>();
        
    }
    public override void DecisionStateUpdate()
    {
        characterText.text = textString;

        string traits = "";
        characterTraits.text = traits;
        
        this.DecisionTree.StartTreeFromBeggining();

    }
}
