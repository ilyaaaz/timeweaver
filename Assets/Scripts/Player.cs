using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    float dx, dy;
    Rigidbody2D rb;
    BoxCollider2D cld;

    public bool isJumping;

    [Header("Player Attributes")]
    public float speed;
    public float jumpForce;
    public float pullForce;//pull rope

    //hook
    float pullTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        TimeTravel();
        ShootHook();
        DragRope();
    }

    void Move()
    {
        //left and right
        dx = Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position + new Vector3(dx, 0, 0) * speed * Time.deltaTime;

        //constrains here -- TODO

        transform.position = newPosition;
    }

    void Jump()
    {
        //jump
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        RaycastHit2D checkJump = Physics2D.Raycast(cld.bounds.center + new Vector3(0, -cld.bounds.extents.y, 0), Vector2.down, 0.1f);
        isJumping = !checkJump.collider;
    }

    void TimeTravel()
    {
        //time
        dy = Input.GetAxis("Vertical");

        //time ability
        GameManager.instance.ChangeAnimationSpeed(dy);
    }

    void ShootHook()
    {
        //Left Click shoot hook
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.hookOut)
            {
                GameManager.instance.currentHook = Instantiate(GameManager.instance.hookPrefab);
                GameManager.instance.hookOut = true;
                pullTime = 1f; //reset pull time
            }
        }
        else if (Input.GetMouseButtonUp(0))//release
        {
            Destroy(GameManager.instance.currentHook);
        }
    }

    void DragRope()
    {
        //right click to drag
        if (GameManager.instance.currentHook && GameManager.instance.isHooked && Input.GetMouseButton(1))
        {
            pullTime += Time.deltaTime;
            Vector3 direction = GameManager.instance.currentHook.transform.position - transform.position;
            direction.Normalize();
            rb.AddForce(direction * pullForce * pullTime);
        }
    }
}
