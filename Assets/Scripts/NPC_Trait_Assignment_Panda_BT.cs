using System.Collections.Generic;
using UnityEngine;

public class NPC_Trait_Assignment_Panda_BT : MonoBehaviour
{
    public List<Trait> movementTraits = new List<Trait>();
    public List<Trait> dialogTraits = new List<Trait>();
    void Start()
    {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC_Panda_BT");
        int randomness = 0;
        foreach(GameObject NPC in NPCs)
        {
            Random.InitState(System.Environment.TickCount + randomness);
            PandaBT_NPC npc = NPC.GetComponent<PandaBT_NPC>();
            npc.traits.Add(movementTraits[Random.Range(0, movementTraits.Count)]);
            npc.traits.Add(dialogTraits[Random.Range(0, dialogTraits.Count)]);
            NPC.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
            randomness++;
        }
    }
}
