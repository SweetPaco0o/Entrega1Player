using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpSpeed = 1f;
    public float SmoothRotation = 0.01f;

    public Transform GroundChecker;
    public float groundSphereRadius= 0.1f;

    public LayerMask WhatIsGround;

    Vector3 _lastvelocity;

    CharacterController _characterController;
    InputController _inputController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>(); 
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
        if (!IsGrounded())
            smoothy = 0.01f;

        velocity.x = Mathf.Lerp(velocity.x, localInput.x * Speed, smoothy);
        velocity.y = GetGravity();
        velocity.z = Mathf.Lerp(velocity.z, localInput.z * Speed, smoothy);

        if (ShouldJump())
        {
            velocity.y = JumpSpeed;
        }
        _lastvelocity = velocity;

        _characterController.Move(velocity * Time.deltaTime);

        if (velocity.magnitude > 0)
        {
            var currentLook = transform.position + transform.forward;
            var lookPointTarget = transform.position + new Vector3(velocity.x, 0, velocity.z);
            var lookPoint = Vector3.Lerp(currentLook, lookPointTarget, SmoothRotation * Time.deltaTime);
            transform.LookAt(lookPoint);
        }
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
