using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputController))]

public class PlayerMove : MonoBehaviour
{
    
    CharacterController _characterController;
    InputController _inputController;
    public float speed = 7f;
    public float defaultSpeed = 7f;
    public float increasedSpeed = 14f;
    public float gravity = -30f;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    public LayerMask WhatIsSlow;
    public LayerMask WhatIsMovingPlatform;

    bool isGrounded;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 move = transform.right * _inputController.InputMove.x + transform.forward * _inputController.InputMove.y;

        _characterController.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y<0)
        {
            Debug.Log("HOLAAA");
            velocity.y = -2f;
        }

        if (IsSlow())
        {
            speed = 1f;
        }
        if (IsMovingPlatform())
        {
            speed = 100f;
        }

        if (_inputController.InputMove.x != 0 || _inputController.InputMove.y != 0)
        {
            Debug.Log("kansdaksdkaskd");
            animator.SetBool(isWalkingHash, true);
        }
        else 
        {
            animator.SetBool(isWalkingHash, false);
        }
        
        if(_inputController.Jumped && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool(isJumpingHash, true);
            Debug.Log("salta salta?");
        }
        else 
        {
            animator.SetBool(isJumpingHash, false);
        }

        velocity.y += gravity * Time.deltaTime;

        _characterController.Move(velocity * Time.deltaTime);

         if (_inputController.Run && (_inputController.InputMove.x != 0 || _inputController.InputMove.y != 0))
        {
            if (speed == defaultSpeed)
            {
                speed = increasedSpeed; 
                animator.SetBool(isRunningHash, true);
            }
        }
        else
        {
            speed = defaultSpeed;
            animator.SetBool(isRunningHash, false);
        }
    }
    private bool IsMovingPlatform()
    {
        return Physics.CheckSphere(groundCheck.position,
            groundDistance,
            WhatIsMovingPlatform);
    }

    private bool IsSlow()
    {
        return Physics.CheckSphere(groundCheck.position,
            groundDistance,
            WhatIsSlow);
    }
}
