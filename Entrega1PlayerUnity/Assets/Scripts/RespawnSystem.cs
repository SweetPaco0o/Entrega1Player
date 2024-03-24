using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public Transform checkpoint;
    public Transform checkpoint2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            transform.position = checkpoint.position;
        }
        else if (other.gameObject.CompareTag("Death2"))
        {
            transform.position = checkpoint2.position;
        }
    }
}