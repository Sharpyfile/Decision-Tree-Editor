using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trait_Assignment_Not_Native : MonoBehaviour
{
    public List<string> movementTraits = new List<string>();
    public List<string> dialogTraits = new List<string>();
    void Start()
    {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC_without_native_traits");
        int randomness = 0;
        foreach(GameObject NPC in NPCs)
        {
            Random.InitState(System.Environment.TickCount + randomness);
            NPC.GetComponent<NPCTraits>().traits.Add(movementTraits[Random.Range(0, movementTraits.Count)]);
            NPC.GetComponent<NPCTraits>().traits.Add(dialogTraits[Random.Range(0, dialogTraits.Count)]);
            Debug.Log(movementTraits[Random.Range(0, movementTraits.Count)]);
            Debug.Log(dialogTraits[Random.Range(0, dialogTraits.Count)]);
            randomness++;
        }
    }

    

}
