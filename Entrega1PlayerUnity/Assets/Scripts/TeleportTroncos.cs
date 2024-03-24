using UnityEngine;

public class TeleportToCheckpointOnCollision : MonoBehaviour
{
    public Transform checkpoint; // Asigna el checkpoint deseado desde el editor de Unity

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tronco")) // Comprueba si el objeto con el que colisionó tiene la etiqueta "Tronco"
        {
            Debug.Log("TRONQUITO");
            // Teletransporta al jugador al checkpoint
            transform.position = checkpoint.position;
        }
    }
}