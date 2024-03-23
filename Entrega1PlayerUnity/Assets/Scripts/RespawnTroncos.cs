using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnTroncos : MonoBehaviour
{
    public Transform spawnPoint; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            
            other.transform.position = spawnPoint.position;
        }
    }
}

