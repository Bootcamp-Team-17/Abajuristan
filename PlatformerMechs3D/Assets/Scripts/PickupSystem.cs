using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupScript : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCam;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObject;
    bool _isPickupPressed;
    
    private void Awake() 
    {
        playerInput.CharacterControls.Pickup.started += onPickup;
    }
    void Start()
    {
        playerInput = new PlayerInput();
    }
    void Update()
    {
        if(_isPickupPressed)
        {
            if(CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject = null;
                return;
            }

            Ray CameraRay = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); 
            if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
            {
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(CurrentObject)
        {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint; 
        }
    }

    void onPickup(InputAction.CallbackContext context)
    {   
        _isPickupPressed = context.ReadValueAsButton();
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