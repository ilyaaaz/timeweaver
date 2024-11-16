using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public System.Action<Collider2D> OnTriggerEnterAction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterAction?.Invoke(other);
        }
        
        Destroy(gameObject);
    }
}