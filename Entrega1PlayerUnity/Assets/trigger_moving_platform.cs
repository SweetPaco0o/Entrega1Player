using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_moving_platform : MonoBehaviour
{
    private GameObject Moving_platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Moving_platform = other.gameObject;
            Moving_platform.transform.parent = transform;
            Debug.Log("Player is now child of platform.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Moving_platform != null)
            {
                Moving_platform.transform.parent = null;
                Moving_platform = null;
                Debug.Log("Player is no longer child of platform.");
            }
        }
    }
    private void FixedUpdate()
    {
        if(Moving_platform != null) 
        {
            Debug.Log("Player local position: " + transform.localPosition);
        }
    }
}