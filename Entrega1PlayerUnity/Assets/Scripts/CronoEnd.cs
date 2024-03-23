using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CronoEnd : MonoBehaviour
{
    public Crono crono;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crono.StopChronometer();
        }
    }
}


