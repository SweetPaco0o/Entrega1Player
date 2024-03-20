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


    private void LateUpdate()
    {
        _jumped = false;
        _run = false;
    }
    private void OnMove(InputValue input)
    {
        _inputMovement = input.Get<Vector2>();
         Debug.Log("Move");
    }

    private void OnJump()
    {
        _jumped = true;
        Debug.Log("Jump");
    }

    private void OnRun()
    {
        _run = true;
        Debug.Log("Run");
    }

    /*private void OnRunStart()
    {
        _run = true;
        Debug.Log("RunStart");
    }
    private void OnRunEnd()
    {
        _run = false;
        Debug.Log("RunEnd");
    }*/
}
