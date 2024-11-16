using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWalk : MonoBehaviour
{
    public int direction;
    public float speed;
    public Collider2D walkOnCld;

    private Vector2[] waypoints;
    private int currentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = walkOnCld.bounds;

        // Calculate the four corners based on the collider's bounds
        waypoints = new Vector2[4];
        waypoints[0] = new Vector2(bounds.min.x, bounds.max.y); // Top-left
        waypoints[1] = new Vector2(bounds.max.x, bounds.max.y); // Top-right
        waypoints[2] = new Vector2(bounds.max.x, bounds.min.y); // Bottom-right
        waypoints[3] = new Vector2(bounds.min.x, bounds.min.y); // Bottom-left
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);

        // Check if the enemy reached the waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            // Move to the next waypoint, looping back to the start if at the last waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = -direction;
        }
    }
}
