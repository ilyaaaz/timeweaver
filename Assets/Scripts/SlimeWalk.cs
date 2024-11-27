using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;

public class SlimeWalk : MonoBehaviour
{
    public int direction; // 1 clockwise, -1 counter clockwise
    float speed;

    public int state; // 1 walk, 2 rotate, 3 air

    float rotateDegree;

    Collider2D cld;
    Rigidbody2D rb;
    //GameObject left;
    //GameObject right;
    GameObject middle;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.speed;
        cld = GetComponent<Collider2D>();
        //left = transform.GetChild(0).gameObject;
        //right = transform.GetChild(1).gameObject;
        middle = transform.GetChild(0).gameObject;
        //left.transform.position = new Vector2(cld.bounds.min.x, cld.bounds.min.y);
        //right.transform.position = new Vector2(cld.bounds.max.x, cld.bounds.min.y);
        middle.transform.position = new Vector2(transform.position.x, cld.bounds.min.y);
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D leftPoint =  Physics2D.Raycast(left.transform.position, -transform.up, 0.1f);
        //RaycastHit2D rightPoint = Physics2D.Raycast(transform.position, -transform.up, 0.1f);
        RaycastHit2D middlePoint = Physics2D.Raycast(transform.position, -transform.up, 0.01f);

        //Debug.DrawLine(left.transform.position, left.transform.position + (-transform.up * 0.1f), Color.red);
        Debug.DrawLine(middle.transform.position, middle.transform.position + (-transform.up * 0.01f), Color.red);


        switch (state) 
        {
            case 1:
                transform.position += transform.right * direction * speed * Time.deltaTime;
                if (middlePoint.collider == null)
                {
                    state = 2;
                }
                break;
            case 2:
                if (direction == -1)
                {
                    transform.Rotate(new Vector3(0, 0, 1.5f));
                }
                else if (direction == 1)
                {
                    transform.Rotate(new Vector3(0, 0, -1.5f));
                }
                rotateDegree += 1.5f;
                if (rotateDegree >= 90 || middlePoint.collider != null)
                {
                    state = 1;
                    rotateDegree = 0;
                }
                break;
        }






        /*
        if (clockWise == 0)
        {
            if (leftPoint.collider == null)
            {
                clockWise = -1;
            } else if (rightPoint.collider == null)
            {
                clockWise = 1;
            }
        }
        */
        /*
        if (leftPoint.collider != null && rightPoint.collider != null)
        {
            clockWise = 0;
        }
        */
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = -direction;
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            state = 1;
            rb.gravityScale = 0;
            sr.sprite = ChangeSpritePivot(sr.sprite, new Vector2(1,0));
            Destroy(cld);
            cld = transform.AddComponent<BoxCollider2D>();
        }
    }


    // Function to change the pivot of a sprite
    public Sprite ChangeSpritePivot(Sprite originalSprite, Vector2 newPivot)
    {
        if (originalSprite == null)
        {
            Debug.LogError("Original sprite is null!");
            return null;
        }

        return Sprite.Create(
            originalSprite.texture,
            originalSprite.rect,
            newPivot,
            originalSprite.pixelsPerUnit,
            0,
            SpriteMeshType.FullRect,
            originalSprite.border
        );
    }
}
