using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeWood : MonoBehaviour
{
    public BridgeSide leftSide;
    public BridgeSide rightSide;
    public Rigidbody2D rb;
    public HingeJoint2D hj;
    public Vector3 originalPos;
    public Vector3 originalRot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        originalPos = transform.position;
        originalRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("DestroyWood", 1f);
        }
    }

    void DestroyWood()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        hj.enabled = false;
        leftSide.woodParts.Add(this);
        rightSide.woodParts.Add(this);
    }
}
