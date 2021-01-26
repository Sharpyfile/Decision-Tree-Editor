using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Collisions : MonoBehaviour
{
    private BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<DecisionTreeComponent>().SetBool("playerInside", true);
            this.GetComponent<DecisionTreeComponent>().SetBool("playerOutside", false);
        }
        else if (other.CompareTag("Dragon"))
        {
            this.GetComponent<DecisionTreeComponent>().SetBool("dragonInside", true);
            this.GetComponent<DecisionTreeComponent>().SetBool("dragonOutside", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<DecisionTreeComponent>().SetBool("playerInside", false);
            this.GetComponent<DecisionTreeComponent>().SetBool("playerOutside", true);
        }
        else if (other.CompareTag("Dragon"))
        {
            this.GetComponent<DecisionTreeComponent>().SetBool("dragonInside", false);
            this.GetComponent<DecisionTreeComponent>().SetBool("dragonOutside", true);
        }
    }
}
