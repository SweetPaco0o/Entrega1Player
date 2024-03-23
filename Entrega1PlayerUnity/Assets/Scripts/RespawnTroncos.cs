using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTroncos : MonoBehaviour
{
    public Vector3 respawnPosition1 = new Vector3(4.22f, 14.63957f, -38.2f); 
    public Vector3 respawnPosition2 = new Vector3(0.87f, 16.5f, 52f); 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión detectada");
            
          
            Vector3 respawnPosition = Random.Range(0, 2) == 0 ? respawnPosition1 : respawnPosition2;
            Debug.Log("Respawn en posición: " + respawnPosition); 

            
            collision.gameObject.transform.position = respawnPosition;
        }
    }
}
