using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOnFloor : MonoBehaviour
{
    LineRenderer lr;
    Vector3[] floorCornors = new Vector3[5];
    Collider2D cld;

    private void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        cld = GetComponent<Collider2D>();
        floorCornors[0] = new Vector3(cld.bounds.min.x, cld.bounds.min.y, 0);
        floorCornors[1] = new Vector3(cld.bounds.max.x, cld.bounds.min.y, 0);
        floorCornors[2] = new Vector3(cld.bounds.max.x, cld.bounds.max.y, 0);
        floorCornors[3] = new Vector3(cld.bounds.min.x, cld.bounds.max.y, 0);
        floorCornors[4] = new Vector3(cld.bounds.min.x, cld.bounds.min.y, 0);
        lr.positionCount = floorCornors.Length;
        lr.SetPositions(floorCornors);
    }


}
