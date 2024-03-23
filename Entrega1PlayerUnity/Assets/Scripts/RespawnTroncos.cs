using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTroncos : MonoBehaviour
{
    public Vector3 respawnPosition1 = new Vector3(4.22f, 14.63957f, -38.2f);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("RespawnTroncos");
            transform.position = respawnPosition1;
        }
    
        else
        {
            Debug.LogWarning("Collision with non-Player object");
        }
}

}
