using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour, IReversible
{
    [SerializeField] private Vector2 targetPosition; // Target position in local space
    [SerializeField] private float speed = 2f;       // Speed of movement

    private bool isReversing = false; // To control movement

    void Update()
    {
        // If reversing, move towards the target position
        if (isReversing)
        {
            MoveTowardsTarget();
        }
    }

    public void ReverseTime()
    {
        isReversing = true; // Enable movement towards the target
    }

    private void MoveTowardsTarget()
    {
        // Smoothly move the object towards the target position in local space
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);

        // Stop movement when the target position is reached
        if (Vector2.Distance(transform.localPosition, targetPosition) < 0.01f)
        {
            isReversing = false; 
        }
    }
}