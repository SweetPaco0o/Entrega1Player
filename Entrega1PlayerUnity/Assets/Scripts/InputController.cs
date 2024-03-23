using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 _inputMovement;
    public Vector2 InputMove { get { return _inputMovement; } }

    private bool _jumped;
    public bool Jumped { get { return _jumped; } }
    
    private bool _run;
    public bool Run { get { return _run; } }

    private bool _zoom;
    public bool Zoom { get { return _zoom; } }
    private bool _camera;
    public bool Camera { get { return _camera; } }


    private void LateUpdate()
    {
        _jumped = false;
        _zoom = false;
        _camera = false;
    }
    private void OnMove(InputValue input)
    {
        _inputMovement = input.Get<Vector2>();
    }

    private void OnJump()
    {
        _jumped = true;
    }

    /*private void OnRun()
    {
        _run = true;
        Debug.Log("Run");
    }*/

    private void OnRunStart()
    {
        _run = true;
    }
    private void OnRunEnd()
    {
        _run = false;
    }

    private void OnZoomStart()
    {
        _zoom = true;
    }
    private void OnZoomEnd()
    {
        _zoom = false;
    }

    private void OnCamera()
    {
        _camera = true;
        Debug.Log("Camara");
    }
}
