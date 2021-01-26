using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Collisions : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Rigidbody playerRigidbody;
    private Rigidbody dragonRigidbody;
    private Transform model;
    public float speed = 3.0f;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        dragonRigidbody = GameObject.FindGameObjectWithTag("Dragon").GetComponent<Rigidbody>();
        model = this.transform.Find("NPCModel");
    }

    void Update()
    {
        if (Vector3.Distance(model.position, playerRigidbody.transform.position) < Vector3.Distance(model.position, dragonRigidbody.transform.position))
        {
            Vector3 targetDirection = playerRigidbody.transform.position - model.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(model.forward, targetDirection, singleStep, 0.0f);
            model.rotation = Quaternion.LookRotation(newDirection);
            model.eulerAngles = new Vector3(0, model.eulerAngles.y, 0);
            
        }
        else
        {
            Vector3 targetDirection = dragonRigidbody.transform.position - model.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(model.forward, targetDirection, singleStep, 0.0f);
            model.rotation = Quaternion.LookRotation(newDirection);
            model.eulerAngles = new Vector3(0, model.eulerAngles.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            this.GetComponent<DecisionTreeComponent>().SetBool("playerInside", true);
            this.GetComponent<DecisionTreeComponent>().SetBool("playerOutside", false);
        }
        else if (other.CompareTag("Dragon"))
        {
            Debug.Log("Dragon");
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
