using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTroncos : MonoBehaviour
{
    public Vector3 respawnPosition1 = new Vector3(4.22f, 14.63957f, -38.2f);
    private Die die;

    private void Start()
    {
        die = GetComponent<Die>();
    }

    private void OnCollisionEnter(Collision collision)
    {
    // Check if the collided object has the "Player" tag
    if (collision.gameObject.CompareTag("Player"))
    {
        Debug.Log("Collision with Player detected");
        

        // Get the Rigidbody component of the collided object
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            

        // Check if the Rigidbody component is not null
        if (playerRigidbody != null)
        {
            // Perform actions using playerRigidbody
            Debug.Log("Player Rigidbody found");
                die.Death();

        }
        else
        {
            Debug.LogWarning("Player Rigidbody not found.");
        }
    }
    else
    {
       // Debug.LogWarning("Collision with non-Player object");
    }
}

}
