using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour, IReversible
{
    public CollisionDetector fallDetector;
    public float reversingTime = 2f;
    [SerializeField] private Vector2 targetPosition; // The target position to move to in local space
    [SerializeField] private float speed = 2f;       // Speed of movement

    private Vector2 originalPosition; // To store the initial position in local space
    private bool isReversing = false; // To control reverse movement
    private bool isMovingToTarget = false; // To control forward movement
    private bool isReturningToTarget = false; // To prevent multiple coroutines

    private void Start()
    {
        // Store the original local position
        originalPosition = transform.localPosition;

        // Register collision handler
        fallDetector.OnTriggerEnterAction += HandleCollision;
    }

    private void Update()
    {
        // Handle forward movement
        if (isMovingToTarget)
        {
            MoveTowards(targetPosition);
        }

        // Handle reverse movement
        if (isReversing)
        {
            MoveTowards(originalPosition);
        }
    }

    private void HandleCollision(Collider2D other)
    {
        isMovingToTarget = true;
        isReversing = false;

        // Unsubscribe from the event to avoid redundant calls
        fallDetector.OnTriggerEnterAction -= HandleCollision;
    }

    public void ReverseTime()
    {
        isReversing = true;
        isMovingToTarget = false; // Stop moving forward if it was
        isReturningToTarget = false; // Reset return flag
    }

    private void MoveTowards(Vector2 target)
    {
        // Smoothly move the object towards the target position in local space
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);

        // Stop moving when the target is reached
        if (Vector2.Distance(transform.localPosition, target) < 0.01f)
        {
            if (isMovingToTarget)
            {
                isMovingToTarget = false;
            }
            else if (isReversing && !isReturningToTarget)
            {
                isReversing = false;

                // Start a coroutine to return to the target position after 2 seconds
                StartCoroutine(ReturnToTargetAfterDelay());
            }
        }
    }

    private IEnumerator ReturnToTargetAfterDelay()
    {
        isReturningToTarget = true; // Prevent multiple coroutines
        yield return new WaitForSeconds(reversingTime); // Wait for 2 seconds
        
        isMovingToTarget = true; // Start moving back to the target position
    }
}
