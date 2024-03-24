using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float slowSpeedMultiplier = 0.3f; // Velocidad reducida cuando pasa por objetos Slow
    private bool isInSlowArea = false; // Indica si el jugador está en un área Slow
    private bool isClimbingRope;

    public float defaultSpeed = 10f;
    public float increasedSpeed = 20f;
    public float JumpSpeed = 4f;
    public float Jumps = 2f;
    public float JumpsLeft = 0f;
    public float SmoothRotation = 0.01f;
    

    public float gravity = -9.81f;
    public float gravityMultiplier = 2f;
    private Vector3 moveDirection = Vector3.zero;

    public Transform GroundChecker;
    public float groundSphereRadius= 0.1f;

    public LayerMask WhatIsGround;

    Vector3 _lastvelocity;

    CharacterController _characterController;
    InputController _inputController;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isClimbingHash;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>(); 

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isClimbingHash = Animator.StringToHash("isClimbing");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 velocity = _lastvelocity;

        Vector3 localInput = transform.right * _inputController.InputMove.x
            + transform.forward * _inputController.InputMove.y;

        float smoothy = 1;

        if(localInput.magnitude > 0 && IsGrounded())
        {
            animator.SetBool(isWalkingHash, true);
        }
        else{
            animator.SetBool(isWalkingHash, false);
        }

        if (!IsGrounded())
        {
            smoothy = 0.01f;
            if (moveDirection.y > 0)
            {
                moveDirection.y -= gravityMultiplier * Time.deltaTime;
                _characterController.Move(moveDirection * Time.deltaTime);
            }
            
        }
        else
        {
            JumpsLeft = Jumps;
        }

        float WalkingSpeed = _inputController.Run ?  increasedSpeed : defaultSpeed;

        if (isInSlowArea)
        {
            WalkingSpeed *= slowSpeedMultiplier;
        }

        velocity.x = Mathf.Lerp(velocity.x, localInput.x * WalkingSpeed, smoothy);
        velocity.y = GetGravity();
        velocity.z = Mathf.Lerp(velocity.z, localInput.z * WalkingSpeed, smoothy);   
            
        if (ShouldJump())
        {
            velocity.y = JumpSpeed;
            --JumpsLeft;
            animator.SetBool(isJumpingHash, true);
        }else{
            animator.SetBool(isJumpingHash, false);
        }

        _lastvelocity = velocity;

        if(!isClimbingRope){
            //Not Climbing Rope
            float avoidFloorDistance = .1f;
            float ropeGrabbDistance = 1f;
            if (Physics.Raycast(transform.position + Vector3.up * avoidFloorDistance, localInput, out RaycastHit raycastHit, ropeGrabbDistance))
            {
                if (raycastHit.transform.TryGetComponent(out Rope rope))
                {
                    GrabRope();  
                    animator.SetBool(isClimbingHash, true);
                }
            }
        }
        else 
        {
            //Climbing the rope
            float avoidFloorDistance = .1f;
            float ropeGrabbDistance = 1f;
            if (Physics.Raycast(transform.position + Vector3.up * avoidFloorDistance, localInput, out RaycastHit raycastHit, ropeGrabbDistance))
            {
                if (!raycastHit.transform.TryGetComponent(out Rope rope))
                {
                    DropRope();
                    animator.SetBool(isClimbingHash, false);
                }
            }
            else
            {
                DropRope();
                animator.SetBool(isClimbingHash, false);
            }
        }
        if (isClimbingRope)
        {
            GrabRope();
            velocity.x = 0f;
            velocity.y = localInput.z * defaultSpeed;
            velocity.z = 0f;
            //Physics.gravity.y = 0;
        }

        _characterController.Move(velocity * Time.deltaTime);

        animator.SetBool(isRunningHash, _inputController.Run);
    }

    private void GrabRope()
    {
        isClimbingRope = true;
    }

    private void DropRope()
    {
        isClimbingRope = false;
    }

    private bool ShouldJump()
    {
        return _inputController.Jumped && (IsGrounded() || JumpsLeft>0);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(
            GroundChecker.position, groundSphereRadius, WhatIsGround);
    }

    private float GetGravity()
    {
        float currentVelocity = _lastvelocity.y;
        currentVelocity += gravity * Time.deltaTime;
        return currentVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            isInSlowArea = true;
            Debug.Log("PISANDO SLOW");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            isInSlowArea = false;
        }
    }
}
