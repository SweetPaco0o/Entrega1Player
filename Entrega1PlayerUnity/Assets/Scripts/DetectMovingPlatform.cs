using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectMovingPlatform : MonoBehaviour
{
    public Transform GroundChecker;
    public float groundSphereRadius = 0.1f;

    public LayerMask WhatIsMovingPlatform;

    private void FixedUpdate()
    {
        AttachParent();
    }
    private void AttachParent()
    {
        if (IsMovingPlatform())
        {
            Debug.Log("En contacto con Moving Platform");
        }
    }

    private bool IsMovingPlatform()
    {
        return Physics.CheckSphere(
            GroundChecker.position, groundSphereRadius, WhatIsMovingPlatform);
    }
}
