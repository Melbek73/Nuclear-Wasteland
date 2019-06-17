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

    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        tedBody = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().size.x;
        tedTransform = this.transform;
        groundLayer.value = 8; // groundlayer has number 8
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lineCastPos = tedTransform.position - tedTransform.right * width;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector3.down, groundLayer);

        if(!isGrounded)
        {

        }

        Vector3 velocity = tedBody.velocity;
        velocity.x = speed;
        tedBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != groundLayer)
        {
            moveRight = !moveRight; // toggle direction
        }
    }
}
