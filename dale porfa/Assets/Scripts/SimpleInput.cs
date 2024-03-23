using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInput : MonoBehaviour
{

    Rigidbody _rigidbody;
    public float Force=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = GetInput();
        Move(input);
    }

    private static Vector3 GetInput()
    {
        Vector3 input = new Vector3();
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        return input;
    }

    private void Move(Vector3 input)
    {        
        _rigidbody.AddForce(input * Force);
    }


}
