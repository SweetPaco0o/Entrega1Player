using UnityEngine;

public class TeleportToCheckpointOnCollision : MonoBehaviour
{
    public Transform checkpoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tronco"))
        {
            transform.position = checkpoint.position;
        }
    }
}