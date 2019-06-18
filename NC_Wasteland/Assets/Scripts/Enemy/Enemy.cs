using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy attributes
    private int health;
    private bool moveRight = false;
    private float width;
    private float height;
    float speed = 1;
    Rigidbody2D tedBody;
    Transform tedTransform;

    // Just for debug
    float actionTime = 0;

    public LayerMask enemyMask;

    private void enemyMove()
    {
        Vector3 lineCastPos = tedTransform.position - tedTransform.right * width;

        Debug.DrawLine(lineCastPos, lineCastPos + Vector3.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector3.down, enemyMask);

        Debug.DrawLine(lineCastPos, lineCastPos - tedTransform.right * 0.02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - tedTransform.right * 0.02f, enemyMask);

        //Debug.Log("Enemy isGrounded=" + isGrounded + " isBlocked=" + isBlocked);
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = tedTransform.eulerAngles;
            currRot.y += 180;
            tedTransform.eulerAngles = currRot;
        }

        Vector3 velocity = tedBody.velocity;
        velocity.x = -tedTransform.right.x * speed;
        tedBody.velocity = velocity;
    }

    void hurt()
    {
        health -= 50;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tedBody = this.GetComponent<Rigidbody2D>();
        tedTransform = this.transform;
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;

        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
        Globals.DebugAfterTime(ref actionTime, 2, "Enemy Health=" + health);
        if(health < 0)
        {
            Debug.Log("Enemy: ICH BIN TOOOOOOD");
        }
    }

   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl player = collision.collider.GetComponent<PlayerControl>();
        if(player != null)
        {
            hurt();
        }
    }
}
