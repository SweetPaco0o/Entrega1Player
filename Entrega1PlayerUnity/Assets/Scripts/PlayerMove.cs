using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed = 7f;
    public float defaultSpeed = 7f;
    public float increasedSpeed = 15f;
    public float gravity = -9.8f;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (x != 0 || z != 0)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else 
        {
            animator.SetBool(isWalkingHash, false);
        }
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool(isJumpingHash, true);
        }
        else 
        {
            animator.SetBool(isJumpingHash, false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

         if (Input.GetButton("Run") && (x != 0 || z != 0))
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

    
}
