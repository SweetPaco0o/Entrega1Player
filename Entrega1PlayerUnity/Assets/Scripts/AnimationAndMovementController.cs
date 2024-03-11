using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 20f;
    public float Speed = 10f;
    public float WalkingSpeed = 3f;
    public float gravity = -2f;
    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.Gameplay.Move.started += onMovementInput;
        playerInput.Gameplay.Move.canceled += onMovementInput;
        playerInput.Gameplay.Move.performed += onMovementInput;
        playerInput.Gameplay.Run.started += onRun;
        playerInput.Gameplay.Run.canceled += onRun;
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
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        } else{
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }
    void Update()
    {
        handleGravity();
        handleRotation();
        handleAnimation();

        if(isRunPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else{
            characterController.Move(currentMovement * Time.deltaTime);
        }
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
