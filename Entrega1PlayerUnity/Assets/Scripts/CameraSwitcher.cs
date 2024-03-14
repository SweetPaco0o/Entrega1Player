using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera ThirdPersonCam;
    public Camera FirstPersonCam;

    void Start()
    {
        ThirdPersonCam.enabled = false;
    }

    void Update()
    {
        SwitchCamera();
    }

    void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (ThirdPersonCam.enabled)
            {
                ThirdPersonCam.enabled = false;
                FirstPersonCam.enabled = true; // Activar la cámara en primera persona
            }
            else
            {
                FirstPersonCam.enabled = false;
                ThirdPersonCam.enabled = true; // Activar la cámara en tercera persona
            }
        }
    }
}