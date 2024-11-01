using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float dx, dy;
    Rigidbody2D rb;
    BoxCollider2D cld;

    public GameObject effectArea;

    //ground Settings
    public bool isGround;
    Vector3 leaveGroundPos;
    bool stopRecordPos;



    float speed;
    //public float maxSpeed;
    //public float deceleration = 5f;
    float jumpForce;
    int health;

    //float pullForce; // Pull rope

    // Hook
    //float pullTime;
    private NPC currentNPC = null; // Track the NPC the player is interacting with

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<BoxCollider2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        GetAttributesFromManager();
        ResetAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        TimeTravel();
        //RecordPosLeaveGround();
        //ShootHook();
        //DragRope();
        CheckHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("111");
        CollideWithTrap(collision);
    }

    private void CollideWithTrap(Collision2D other)
    {
        if ((GameManager.instance.trapLayer & (1 << other.gameObject.layer)) != 0)
        {
            health--;
            transform.position = leaveGroundPos;
        } else
        {
            if (isGround)
            {
                RecordPosLeaveGround();
            }
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            //waiting to be implemented
        }
    }

    void RecordPosLeaveGround()
    {
        /*
        RaycastHit2D checkGround = Physics2D.Raycast((cld.bounds.center - new Vector3(0, cld.bounds.extents.y, 0)), Vector3.down, 0.1f, GameManager.instance.groundLayer);
        if (checkGround.collider)
        {
            Debug.Log(leaveGroundPos);
            leaveGroundPos = transform.position;
        }
        */
        leaveGroundPos = transform.position;
    }

        void SetEffectArea()
    {
        effectArea.transform.localScale = Vector3.one * GameManager.instance.effectAreaRadius;
    }
    
    void GetAttributesFromManager()
    {
        speed = GameManager.instance.playerSpeed;
        jumpForce = GameManager.instance.playerJumpForce;
        SetEffectArea();
        //pullForce = GameManager.instance.playerPullForce;
    }

    void ResetAttributes()
    {
        health = GameManager.instance.playerHealth;
    }

    void Move()
    {
        /*
        float input = Input.GetAxis("Horizontal");

        // If there's input, update dx (apply acceleration)
        if (input != 0)
        {
            dx = input * speed;
        }
        else
        {
            // If no input, apply deceleration (reduce dx towards zero)
            dx = Mathf.MoveTowards(dx, 0, deceleration * Time.deltaTime);
        }

        // Move the character by dx
        Vector3 newPosition = transform.position + new Vector3(dx, 0, 0) * Time.deltaTime;

        // Apply new position
        transform.position = newPosition;
        */

        dx = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dx * speed, rb.velocity.y);
    }

    void Jump()
    {
        // Jump
        if (isGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            // Apply only vertical force for jump
            //rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset the vertical velocity before jumping to avoid stacking forces
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Use Impulse to apply a sudden jump force
        }

        // Ground check
        RaycastHit2D checkJump = Physics2D.BoxCast(cld.bounds.center, cld.bounds.size, 0, Vector2.down, 0.1f, GameManager.instance.groundLayer);
        isGround = checkJump.collider;
    }

    void TimeTravel()
    {

        GameManager.instance.ChangeAnimationSpeed(dy);

        if (Input.GetKeyDown(KeyCode.Space) && !effectArea.activeSelf){
            StartCoroutine(ShowEffectArea());
        }

        if (currentNPC != null)
        {
            if (dy < 0) // Player is going back in time
            {
                currentNPC.GoBackInTime();
            }
            else if (dy > 0) // Player is going forward in time
            {
                currentNPC.AdvanceInTime();
            }
        }
    }

    IEnumerator ShowEffectArea()
    {
        effectArea.transform.position = transform.position;
        effectArea.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.skillCooldown);
        effectArea.SetActive(false);
        GameManager.instance.reversibleObjects.Clear(); //reset reversible objects list
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
    }


    /*
    void ShootHook()
    {
        // Left Click shoot hook
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.hookOut)
            {
                GameManager.instance.currentHook = Instantiate(GameManager.instance.hookPrefab);
                GameManager.instance.hookOut = true;
                pullTime = 1f; // Reset pull time
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Release
        {
            Destroy(GameManager.instance.currentHook);
        }
    }

    void DragRope()
    {
        // Right click to drag
        if (GameManager.instance.currentHook && GameManager.instance.isHooked && Input.GetMouseButton(1))
        {
            pullTime += Time.deltaTime;
            Vector3 direction = GameManager.instance.currentHook.transform.position - transform.position;
            direction.Normalize();
            rb.AddForce(direction * pullForce * pullTime);
        }
    }
    */
}