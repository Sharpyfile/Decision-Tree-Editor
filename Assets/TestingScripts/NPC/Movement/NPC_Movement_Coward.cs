using UnityEngine;
using UnityEngine.UI;

public class NPC_Movement_Coward : DecisionState
{
    private string textString = "A DRAGON! AAAAAAAAAAAAAAAAAAAAAAA!";
    private TextMesh characterText;
    private TextMesh characterTraits;
    private Rigidbody rigidbody;
    private GameObject dragon;
    float force = 3.0f;

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

        dragon = GameObject.Find("Dragon");

        rigidbody = this.DecisionTree.GetComponent<Rigidbody>();
        
    }
    public override void DecisionStateUpdate()
    {

        characterText.text = textString;

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);       

        Vector3 dir = dragon.transform.position - this.DecisionTree.transform.position;
        
        dir = -dir.normalized;
        Debug.Log(dir);
        rigidbody.AddForce(dir * force);
        this.DecisionTree.SetFloat("dragonDistance", Vector3.Distance(this.DecisionTree.transform.position, dragon.transform.position));

        string traits = "";
        foreach(Trait trait in this.DecisionTree.traits)  
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        
    }
}
