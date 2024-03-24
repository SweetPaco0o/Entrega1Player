using UnityEngine;

public class TeleportToCheckpoint : MonoBehaviour
{
    public Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = checkpoint.position;
        }
    }
}