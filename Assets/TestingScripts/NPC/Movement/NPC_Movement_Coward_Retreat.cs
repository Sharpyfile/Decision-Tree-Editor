using UnityEngine;
using UnityEngine.UI;

public class NPC_Movement_Coward_Retreat : DecisionState
{
    private string textString = "GET AWAY FROM ME!";
    private Text characterText;
    private Text characterTraits;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    public override void DecisionStateStart()
    {
        characterText = GameObject.Find("CharacterText").GetComponent<Text>();
        characterTraits = GameObject.Find("CharacterTraits").GetComponent<Text>();
        rigidbody = this.DecisionTree.GetComponent<Rigidbody>();
        
    }
    public override void DecisionStateUpdate()
    {
        Debug.Log(this.GetType().ToString());
        if (Input.GetKey(KeyCode.Space))
            characterText.text = textString;

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);       

        string traits = "";
        foreach(Trait trait in this.DecisionTree.traits)  
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        
    }
}
