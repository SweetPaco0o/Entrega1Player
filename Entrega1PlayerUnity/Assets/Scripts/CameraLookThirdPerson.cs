using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookThirdPerson : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;
    float xRotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}