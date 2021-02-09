using UnityEngine;
using System.Collections.Generic;
using Panda;


public class PandaBT_NPC : MonoBehaviour
{
    public List<Trait> traits;
    private Rigidbody rigidbody;
    private GameObject dragon;
    private Rigidbody dragonRigidbody;
    private GameObject knight;
    private Rigidbody knightRigidbody;
    private Material material;
    private TextMesh characterText;
    private TextMesh characterTraits;
    private Transform model;
    private bool isNearKnightVar = false;
    private bool isNearDragonVar = false;
    private float dragonDistance;
    private bool punchedDragon = false;

    private void Start()
    {
        foreach(TextMesh textMesh in this.GetComponentsInChildren<TextMesh>())
        {
            if (textMesh.name == "NPC_Dialog")
                characterText = textMesh;
            else if (textMesh.name == "NPC_Traits")
                characterTraits = textMesh;
        }

        dragon = GameObject.Find("Dragon");
        dragonRigidbody = dragon.GetComponent<Rigidbody>();
        knight = GameObject.Find("Player");
        knightRigidbody = knight.GetComponent<Rigidbody>();

        rigidbody = this.GetComponent<Rigidbody>();
        material = this.transform.Find("NPCModel").GetComponent<Renderer>().material;
        model = this.transform.Find("NPCModel");


}

    private void Update()
    {
        if (Vector3.Distance(model.position, knightRigidbody.transform.position) < Vector3.Distance(model.position, dragonRigidbody.transform.position))
        {
            Vector3 targetDirection = knightRigidbody.transform.position - model.position;
            float singleStep = 3.0f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(model.forward, targetDirection, singleStep, 0.0f);
            model.rotation = Quaternion.LookRotation(newDirection);
            model.eulerAngles = new Vector3(0, model.eulerAngles.y, 0);

        }
        else
        {
            Vector3 targetDirection = dragonRigidbody.transform.position - model.position;
            float singleStep = 3.0f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(model.forward, targetDirection, singleStep, 0.0f);
            model.rotation = Quaternion.LookRotation(newDirection);
            model.eulerAngles = new Vector3(0, model.eulerAngles.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKnightVar = true;
        }
        else if (other.CompareTag("Dragon"))
        {
            isNearDragonVar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKnightVar = false;
        }
        else if (other.CompareTag("Dragon"))
        {
            isNearDragonVar = false;
        }
    }

    [Task]
    bool IsNearKnight()
    {
        return isNearKnightVar;
    }

    [Task]
    bool IsNearDragon()
    {
        return isNearDragonVar;
    }

    [Task]
    void DialogSpokojny()
    {
        material.SetColor("_Fresnel_Color", Color.green);
        if (Input.GetKey(KeyCode.Space))
            characterText.text = "How are you?";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 2.0f, ForceMode.Impulse);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void DialogRozmowny()
    {
        material.SetColor("_Fresnel_Color", Color.green);
        if (Input.GetKey(KeyCode.Space))
            characterText.text = "Hello, how are you? Isnt it a great day?";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 3.0f, ForceMode.Impulse);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void DialogGniewny()
    {
        material.SetColor("_Fresnel_Color", Color.green);
        if (Input.GetKey(KeyCode.Space))
            characterText.text = "Get out of my sight now!";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void DialogWstydliwy()
    {
        material.SetColor("_Fresnel_Color", Color.green);
        if (Input.GetKey(KeyCode.Space))
            characterText.text = "I would rather not talk...";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void MovementCoward()
    {
        material.SetColor("_Fresnel_Color", Color.yellow);
        characterText.text = "A DRAGON! AAAAAAAAAAAAAAAAAAAAAAA!";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        Vector3 dir = dragon.transform.position - this.transform.position;

        dir = -dir.normalized;
        rigidbody.AddForce(dir * 10.0f);

        this.dragonDistance = Vector3.Distance(this.transform.position, dragon.transform.position);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void MovementCowardRetreat()
    {
        characterText.text = "GET AWAY FROM ME!";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        Vector3 dir = dragon.transform.position - this.transform.position;

        dir = -dir.normalized;
        rigidbody.AddForce(dir * 3.0f);

        this.dragonDistance = Vector3.Distance(this.transform.position, dragon.transform.position);

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void MovementBrave()
    {
        material.SetColor("_Fresnel_Color", Color.red);
        characterText.text = "I will slay that dragon!";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        Vector3 dir = dragon.transform.position - this.transform.position;

        dir = dir.normalized;
        dragonRigidbody.AddForce(dir * 5.0f, ForceMode.Impulse);

        this.dragonDistance = Vector3.Distance(this.transform.position, dragon.transform.position);
        if (this.dragonDistance > 5.0f)
            punchedDragon = true;

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void MovementBraveRetreat()
    {
        characterText.text = "AND NEVER COME BACK!";

        if (rigidbody.IsSleeping())
            rigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        this.dragonDistance = Vector3.Distance(this.transform.position, dragon.transform.position);
        if (dragonDistance > 5.0f)
            punchedDragon = false;

        string traits = "";
        foreach (Trait trait in this.traits)
        {
            traits += trait.traitName + "\n";
        }
        characterTraits.text = traits;
        Task.current.Succeed();
    }

    [Task]
    void Idle()
    {
        material.SetColor("_Fresnel_Color", Color.black);
        characterText.text = "";
        characterTraits.text = "";
        Task.current.Succeed();
    }

    [Task]
    bool DragonDistance(float distance)
    {
        if (distance > this.dragonDistance)
            return true;
        else
            return false;
    }

    [Task]
    bool HasPunchedDragon()
    {
        return punchedDragon;
    }

    [Task]
    bool HasTrait(string traitName)
    {
        foreach(Trait trait in traits)
        {
            if (trait.traitName == traitName)
                return true;
        }

        return false; 
    }

}
