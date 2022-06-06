using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isRunningHash;
    int _isjumpingHash;
    int _jumpCountHash;
    //int zero = 0;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentRunMovement;
    Vector3 _appliedMovement;
    bool _isMovementPressed;

    bool _isRunPressed;

    bool _isJumpPressed = false;
    bool _isJumping = false;
    bool _isJumpAnimating = false;

    float _initialJumpVelocity;


    [SerializeField] float gravity = -9.8f;
    [SerializeField] float groundedGravity = -.05f;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = 0.75f;
    [SerializeField] float rotationFactorPerFrame = 15.0f;
    [SerializeField] float characterSpeed;
    [SerializeField] float runSpeed;

    int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    private void Awake() 
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isjumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        //Controller(Gamepad) için gerekli performed
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariable();

    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        

        if(_isRunPressed)
        {
            _appliedMovement.x = _currentRunMovement.x;
            _appliedMovement.z = _currentRunMovement.z;
        }
        else
        {
            _appliedMovement.x = _currentMovement.x;
            _appliedMovement.z = _currentMovement.z;
        }

        characterController.Move(_appliedMovement * Time.deltaTime);

        handleGravity();
        handleJump();
    }

    void onRun (InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
    void onJump (InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }
    void onMovementInput (InputAction.CallbackContext context)
    {
            _currentMovementInput = context.ReadValue<Vector2>();
            _currentMovement.x = _currentMovementInput.x * characterSpeed;
            _currentMovement.z = _currentMovementInput.y * characterSpeed;
            _currentRunMovement.x = _currentMovementInput.x * runSpeed;
            _currentRunMovement.z = _currentMovementInput.y * runSpeed;
            //currentMovement.x = _isRunPressed ? _currentMovementInput.x : _currentMovementInput.x * runSpeed;
            //currentMovement.z = _isRunPressed ? _currentMovementInput.y : _currentMovementInput.y * runSpeed;
            _isMovementPressed = _currentMovementInput.x !=0 || _currentMovementInput.y !=0;
    }

    void setupJumpVariable()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (maxJumpHeight + 2f)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float secondJumpInitialVelocity = (2 * (maxJumpHeight + 2) / (timeToApex * 1.25f));
        float thirdJumpGravity = (-2 * (maxJumpHeight + 4f)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (maxJumpHeight + 4f) / (timeToApex * 1.5f));

        initialJumpVelocities.Add(1, _initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        jumpGravities.Add(0, gravity);
        jumpGravities.Add(1, gravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);

    }

    void handleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        if(_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if(!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        if((_isMovementPressed && _isRunPressed) && !isRunning) 
        {
            _animator.SetBool(_isRunningHash, true);
        }

        if((!_isMovementPressed || !_isRunPressed) && isRunning) 
        {
            _animator.SetBool(_isRunningHash, false);
        }
        
     }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
        
    }

    void handleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f ||!_isJumpPressed;
        float fallMultiplier = 2.0f;
        if(characterController.isGrounded)
        {
            if(_isJumpAnimating)
            {
                _animator.SetBool(_isjumpingHash, false);
                _isJumpAnimating = false;
                currentJumpResetRoutine = StartCoroutine(jumpResetRountine());
                if(jumpCount == 3)
                {
                    jumpCount = 0;
                    _animator.SetInteger(_jumpCountHash, jumpCount);
                }
            }
            _currentMovement.y = groundedGravity;
            _appliedMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (jumpGravities[jumpCount] * fallMultiplier * Time.deltaTime);
            _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (jumpGravities[jumpCount] * Time.deltaTime);
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;

        }
    }

    void handleJump()
    {
        if(!_isJumping && characterController.isGrounded && _isJumpPressed)
        {
            if(jumpCount < 3 && currentJumpResetRoutine !=null)
            {
                StopCoroutine(currentJumpResetRoutine);
            }
            _animator.SetBool(_isjumpingHash, true);
            _isJumpAnimating = true;
            _isJumping = true;
            jumpCount += 1;
            _animator.SetInteger(_jumpCountHash, jumpCount);
            _currentMovement.y = initialJumpVelocities[jumpCount];
            _appliedMovement.y = initialJumpVelocities[jumpCount];
        }
        else if(!_isJumpPressed && _isJumping && characterController.isGrounded)
        {
            _isJumping = false;
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

    IEnumerator jumpResetRountine()
    {
        yield return new WaitForSeconds(.5f);
        jumpCount = 0;
    }
}
