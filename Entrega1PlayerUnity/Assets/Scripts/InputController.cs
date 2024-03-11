using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputController : MonoBehaviour
{
    private Vector2 _input;
    public Vector2 Input { get { return _input; } }

    private bool _jumped;
    
    public bool Jumped { get { return _jumped; } }


    private void LateUpdate()
    {
        _jumped = false;
    }
    private void OnMove(InputValue input)
    {
        _input = input.Get<Vector2>();
    }

    void OnJump()
    {
        _jumped = true;
    }
}
