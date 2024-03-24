using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectMovingPlatform : MonoBehaviour
{
    public Transform GroundChecker;
    public float groundSphereRadius = 0.1f;

    public LayerMask WhatIsMovingPlatform;

    private Transform currentMovingPlatform;
    private Vector3 platformOffset;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        UpdateParenting();
    }

    private void UpdateParenting()
    {
        if (IsMovingPlatform())
        {
            Transform movingPlatform = GetMovingPlatform();
            platformOffset = movingPlatform.position - transform.position;
            currentMovingPlatform = movingPlatform;
        }
        else
        {
            platformOffset = Vector3.zero;
            currentMovingPlatform = null;
        }
        characterController.Move(platformOffset);
    }

    private bool IsMovingPlatform()
    {
        return Physics.CheckSphere(
            GroundChecker.position, groundSphereRadius, WhatIsMovingPlatform);
    }

    private Transform GetMovingPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundSphereRadius * 2, WhatIsMovingPlatform))
        {
            return hit.collider.transform;
        }
        return null;
    }
}
