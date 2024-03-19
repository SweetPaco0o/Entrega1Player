using UnityEngine;

public class PlayerMovingPlatformInteraction : MonoBehaviour
{
    private Transform originalParent; // Store original parent of player

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moving_platform"))
        {
            // Set player's parent to the moving platform
            originalParent = transform.parent; // Store original parent
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset player's parent regardless of the collision object
        transform.parent = originalParent; // Restore original parent
    }
}