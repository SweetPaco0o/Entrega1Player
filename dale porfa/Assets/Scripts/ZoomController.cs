using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    InputController _inputController;
    public Camera FirstPersonCam;
    public float zoomedInFOV = 20f;
    public float defaultFOV = 80f;
    public float zoomSpeed = 5f;

    private bool isZoomed = false;
    private float targetFOV;

    void Start()
    {
        targetFOV = defaultFOV;
        _inputController = GetComponent<InputController>(); 
    }


    void Update()
    {
        ZoomAction();
    }
    void ZoomAction()
    {
        if (_inputController.Zoom)
            {
                isZoomed = !isZoomed;
                targetFOV = isZoomed ? zoomedInFOV : defaultFOV;
            }

        // Interpola suavemente entre el campo de visión actual y el campo de visión objetivo
        FirstPersonCam.fieldOfView = Mathf.Lerp(FirstPersonCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }
}
