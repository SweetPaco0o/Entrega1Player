using UnityEngine;

public class RespawnSystem2 : MonoBehaviour
{
    public Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death2"))
        {
            transform.position = checkpoint.position;
        }
    }
}