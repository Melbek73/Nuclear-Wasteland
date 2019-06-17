using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy attributes
    private int health;
    private bool moveRight = false;
    private float width;
    float speed = 1;
    Rigidbody2D tedBody;
    Transform tedTransform;

    public LayerMask enemyMask;

    // Start is called before the first frame update
    void Start()
    {
        tedBody = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        tedTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lineCastPos = tedTransform.position - tedTransform.right * width;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector3.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector3.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - tedTransform.right * 0.02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - tedTransform.right, enemyMask);
        Debug.Log("Enemy isGrounded=" + isGrounded);
        if(!isGrounded || isBlocked)
        {
            Vector3 currRot = tedTransform.eulerAngles;
            currRot.y += 180;
            tedTransform.eulerAngles = currRot;
        }

        Vector3 velocity = tedBody.velocity;
        velocity.x = -tedTransform.right.x * speed;
        tedBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != enemyMask)
        {
            moveRight = !moveRight; // toggle direction
        }
    }
}
