using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //based on iHeartGameDev YT tutorials
    //reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // variables to sore optimzed parameter IDs  
    int isWalkingHash;
    int isRunningHash;
    int isJummpingHash;

    //vairables to store player movement
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;

    //constants
    float rotationFactorPerFrame = 8f;
    public float Speed = 10f;
    public float WalkingSpeed = 4f;

    //GroundChecker
    public Transform GroundChecker;
    public float groundSphereRadius = 2f;

    //death and slow detection
    public LayerMask WhatIsDeath;
    public LayerMask WhatIsSlow;

    //gravity
    float groundedGravity = -.05f;
    public float gravity = -3f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 4f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool isJumpAnimating = false;

    void Awake()
    {
        //set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //set hash parameters
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJummpingHash = Animator.StringToHash("isJumping");

        //callbacks
        playerInput.Gameplay.Move.started += onMovementInput;
        playerInput.Gameplay.Move.canceled += onMovementInput;
        playerInput.Gameplay.Move.performed += onMovementInput;
        playerInput.Gameplay.Run.started += onRun;
        playerInput.Gameplay.Run.canceled += onRun;
        playerInput.Gameplay.Jump.started += onJump;
        playerInput.Gameplay.Jump.canceled += onJump;

        setupJumpVariables();
    }
    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed){
            animator.SetBool(isJummpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            currentMovement.y = initialJumpVelocity * .5f;
            currentRunMovement.y = initialJumpVelocity * .5f;
        } else if (!isJumpPressed && isJumping && characterController.isGrounded){
            isJumping = false;
        }
    }
    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    void onRun (InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }
    void handleRotation(){
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * WalkingSpeed;
        currentMovement.z = currentMovementInput.y * WalkingSpeed;
        currentRunMovement.x = currentMovementInput.x * Speed;
        currentRunMovement.z = currentMovementInput.y * Speed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }

        else if (!isMovementPressed && isWalking){
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool(isRunningHash, true);
        }

        else if ((!isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool(isRunningHash, false);
        }
    }
    void handleGravity()
    {
        if(characterController.isGrounded){
            if(isJumpAnimating){
                animator.SetBool(isJummpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else{
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }
    void Update()
    {
        handleRotation();
        handleAnimation();

        if(isRunPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else{
            characterController.Move(currentMovement * Time.deltaTime);
        }

        handleGravity();
        handleJump();

        if (IsDeath())
        {
            Speed = 1000f;
        }
        if (IsSlow())
        {
            Speed = Speed * 0.5f;
        }
    }

    private bool IsSlow()
    {
        return Physics.CheckSphere(GroundChecker.position,
            groundSphereRadius,
            WhatIsSlow);
    }

    private bool IsDeath()
    {
        return Physics.CheckSphere(GroundChecker.position,
            groundSphereRadius,
            WhatIsDeath);
    }

    void OnEnable()
    {
        playerInput.Gameplay.Enable();
    }
    void OnDisable()
    {
        playerInput.Gameplay.Disable();
    }
}
