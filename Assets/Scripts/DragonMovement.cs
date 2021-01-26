using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    public float velocity = 0.5f;
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<SwitchModel>().model == SwitchModel.CharacterModel.Dragon)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.AddForce(Vector3.forward * velocity, ForceMode.Impulse);
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigidbody.AddForce(Vector3.back * velocity, ForceMode.Impulse);
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.AddForce(Vector3.left * velocity, ForceMode.Impulse);
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.AddForce(Vector3.right * velocity, ForceMode.Impulse);
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0f);
            }
            if(!Input.anyKey)
            {
                if (rigidbody.velocity.magnitude < 0.3f)
                {
                    rigidbody.velocity = Vector3.zero;
                }
                
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, -45.0f, 0f);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0f);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                rigidbody.transform.rotation = Quaternion.Euler(0.0f,-135.0f, 0f);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                rigidbody.transform.rotation = Quaternion.Euler(0.0f, 135.0f, 0f);
            }
        }   
        else
        {
            if(!Input.anyKey)
            {
                if (rigidbody.velocity.magnitude < 0.3f)
                {
                    rigidbody.velocity = Vector3.zero;
                }
                
            }
        }    
    }
}
