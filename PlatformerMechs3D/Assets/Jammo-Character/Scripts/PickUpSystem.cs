using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    PlayerInput playerInput;
    public GameObject player;
    public Transform targetTransform;
    [SerializeField] Vector3 offset = new Vector3(1, 1, 1);
    Rigidbody rb;

    bool _isPickUpPressed = false;
    public bool playerinRange;
    public bool objectPickedUp;

    private void Awake() {

        playerInput = new PlayerInput(); 
        rb = GetComponent<Rigidbody>();
    
        playerInput.CharacterControls.Pickup.started += onPickup;
        playerInput.CharacterControls.Pickup.canceled += onPickup;
    }

    private void Start()
    {
 
    }

    private void Update()
    {
       if (playerinRange)
        {
            if (_isPickUpPressed)
            {
                objectPickedUp = true;
            }
        }

       if(_isPickUpPressed && transform.position == targetTransform.position)
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
            transform.rotation = transform.rotation;
            rb.useGravity = true;
        }
    }

    void onPickup (InputAction.CallbackContext context)
    {
        _isPickUpPressed = context.ReadValueAsButton();
        Debug.Log("Bastım");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinRange = true;
            Debug.Log("İçerde");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerinRange = false;
            Debug.Log("Dışarda");
        }
    }

    private void OnEnable() 
    {
        playerInput.CharacterControls.Enable();    
    }

    private void OnDisable() 
    {
        playerInput.CharacterControls.Disable();
    }
}
