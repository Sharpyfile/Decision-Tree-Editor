using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : DecisionState
{
    // Start is called before the first frame update
    private string textString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi mollis vitae magna eu dictum. Mauris molestie euismod ante sed venenatis. Sed ut nibh vel nisi tincidunt dignissim et a augue. Pellentesque vitae sagittis lectus. Pellentesque et pulvinar neque. Nunc vel erat posuere orci dignissim congue vitae quis sapien. In ullamcorper enim nec libero tincidunt, in consequat nunc placerat. Phasellus lobortis ligula ante, nec porta sapien tempor ac. Suspendisse volutpat, sem ut aliquet dignissim, risus ante tempus enim, id scelerisque ipsum arcu hendrerit urna. Fusce imperdiet, quam at hendrerit venenatis, nisl nisi eleifend arcu, quis maximus nunc dolor eget metus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam faucibus lorem a posuere varius.";
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
        if (Input.GetKey(KeyCode.Space))
            characterText.text = textString;

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 1.0f, ForceMode.Impulse);  

        string traits = "";
        foreach(Trait trait in this.DecisionTree.traits)  
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
    }
}
