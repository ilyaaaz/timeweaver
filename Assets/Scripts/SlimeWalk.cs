using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;

public class SlimeWalk : MonoBehaviour
{

    public int direction; // 1 clockwise, -1 counter clockwise
    float speed;

    public Collider2D floorCld;
    Vector3[] floorCornors = new Vector3[4];

    public int targetIndex;


    private void Start()
    {
        speed = GameManager.instance.slimeSpeed;
        floorCornors[0] = new Vector3(floorCld.bounds.min.x, floorCld.bounds.min.y, 0);
        floorCornors[1] = new Vector3(floorCld.bounds.max.x, floorCld.bounds.min.y, 0);
        floorCornors[2] = new Vector3(floorCld.bounds.max.x, floorCld.bounds.max.y, 0);
        floorCornors[3] = new Vector3(floorCld.bounds.min.x, floorCld.bounds.max.y, 0);
        //floorCornors[4] = new Vector3(floorCld.bounds.min.x, floorCld.bounds.min.y, 0);
        float minDistance = float.MaxValue;
        for (int i = 0; i < floorCornors.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, floorCornors[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetIndex = i;
            }
        }
    }

    private void Update()
    {
        //Debug.Log(speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, floorCornors[targetIndex], speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, floorCornors[targetIndex]) < 0.1f)
        {
            transform.Rotate(new Vector3(0,0,90f * direction));
            targetIndex += direction;
            targetIndex = (targetIndex + 4) % 4;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            direction = -direction;
            targetIndex += direction;
            targetIndex = (targetIndex + 4) % 4;
        }
    }
}
