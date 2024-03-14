using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    public Camera FirstPersonCam;
    public float zoomedInFOV = 20f;
    public float defaultFOV = 80f;
    void Update()
    {
        Zoom();
    }
    void Zoom()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Activa o desactiva el zoom según el estado actual de la cámara
            if (FirstPersonCam.fieldOfView == defaultFOV)
            {
                FirstPersonCam.fieldOfView = zoomedInFOV;
            }
            else
            {
                FirstPersonCam.fieldOfView = defaultFOV;
            }
        }
    }
}
