using System.Collections.Generic;
using UnityEngine;

public class NPC_Trait_Assignment : MonoBehaviour
{
    public List<Trait> movementTraits = new List<Trait>();
    public List<Trait> dialogTraits = new List<Trait>();
    void Start()
    {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC");
        int randomness = 0;
        foreach(GameObject NPC in NPCs)
        {
            Random.InitState(System.Environment.TickCount + randomness);
            DecisionTreeComponent tree = NPC.GetComponent<DecisionTreeComponent>();
            tree.traits.Add(movementTraits[Random.Range(0, movementTraits.Count)]);
            tree.traits.Add(dialogTraits[Random.Range(0, dialogTraits.Count)]);
            NPC.GetComponent<Renderer>().material.color = Random.ColorHSV();
            randomness++;
        }
    }
}
