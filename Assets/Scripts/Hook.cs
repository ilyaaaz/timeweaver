using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float hookLaunchingSpeed;
    Rigidbody2D rb;
    PolygonCollider2D cld;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<PolygonCollider2D>();
        transform.position = GameManager.instance.player.transform.position;
        ShootOff();
        CreateRope();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //move hook
    void ShootOff()
    {
        //direction
        Vector3 mousePos = GameManager.instance.getMousePos();
        Vector3 direction = (mousePos - transform.position).normalized;
        //add lauching force
        rb.AddForce(direction * hookLaunchingSpeed);
    }

    //turn on cld after exit the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            cld.isTrigger = false;
        }
    }

    //destroy if out of the room
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //check if hit walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            Hooked();
        }
    }

    //create rope
    void CreateRope()
    {
        Instantiate(GameManager.instance.ropePrefab);
    }

    void Hooked()
    {
        rb.velocity = Vector3.zero;
        Destroy(rb);
        GameManager.instance.isHooked = true;
    }
}
