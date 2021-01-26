﻿using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk_Gniewny : DecisionState
{private string textString = "Get out of my sight now!";
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
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);       

        string traits = "";
        foreach(Trait trait in this.DecisionTree.traits)  
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        
    }
}
