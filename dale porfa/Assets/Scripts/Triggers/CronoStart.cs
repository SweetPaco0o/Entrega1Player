using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CronoStart : MonoBehaviour
{
    public Crono crono;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crono.StartChronometer();
        }
    }
}

