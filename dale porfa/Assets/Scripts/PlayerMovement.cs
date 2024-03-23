using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10f;
    public float increasedSpeed = 20f;
    public float JumpSpeed = 4f;
    public float SmoothRotation = 0.01f;

    public Transform GroundChecker;
    public float groundSphereRadius= 0.1f;

    public LayerMask WhatIsGround;

    Vector3 _lastvelocity;

    CharacterController _characterController;
    InputController _inputController;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
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
        }

        float WalkingSpeed = _inputController.Run ?  increasedSpeed : defaultSpeed;
        velocity.x = Mathf.Lerp(velocity.x, localInput.x * WalkingSpeed, smoothy);
        velocity.y = GetGravity();
        velocity.z = Mathf.Lerp(velocity.z, localInput.z * WalkingSpeed, smoothy);   
            
        if (ShouldJump())
        {
            velocity.y = JumpSpeed;
            animator.SetBool(isJumpingHash, true);
        }else{
            animator.SetBool(isJumpingHash, false);
        }

        _lastvelocity = velocity;

        _characterController.Move(velocity * Time.deltaTime);

        animator.SetBool(isRunningHash, _inputController.Run);
    }

    private bool ShouldJump()
    {
        return _inputController.Jumped && IsGrounded();
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(
            GroundChecker.position, groundSphereRadius, WhatIsGround);
    }

    private float GetGravity()
    {
        float currentVelocity = _lastvelocity.y;
        currentVelocity += Physics.gravity.y * Time.deltaTime;
        return currentVelocity;
    }
}
