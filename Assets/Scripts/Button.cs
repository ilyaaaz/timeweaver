using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject targetObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the triggering collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // Destroy the target object if it exists
            if (targetObject != null)
            {
                Destroy(targetObject);
            }
            else
            {
                Debug.LogWarning("Target object is not assigned in the Inspector.");
            }
        }
    }
}