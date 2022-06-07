using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupSystem : MonoBehaviour
{
    public GameObject player;
    public Transform targetTransform;
    [SerializeField] Vector3 offset = new Vector3(1, 1, 1);
    Rigidbody rb;

    public bool playerinRange;
    public bool objectPickedUp;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       if (playerinRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                objectPickedUp = true;
            }
        }

       if(Input.GetKeyDown(KeyCode.E) && transform.position == targetTransform.position)
        {
           objectPickedUp = false;
        }

    }

    private void LateUpdate()
    {
        if (objectPickedUp)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
            rb.useGravity = false;
        }
        else if(!objectPickedUp)
        {
            transform.position = transform.position;
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerinRange = false;
        }
    }
}