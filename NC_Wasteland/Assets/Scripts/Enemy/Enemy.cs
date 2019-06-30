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
    public GameObject explosion;

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

    private void hurt()
    {
        health -= 100;
        if (health <= 0)
        {
            //ToDo add enemy deathsound!!!
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
        Vector3 lineCastPos = tedTransform.up * height + tedTransform.position - tedTransform.right * width / 6;
        Debug.DrawLine(lineCastPos, lineCastPos + tedTransform.right * width * 0.45f * 2);
    }

    public bool isPlayerJumpedOnEnemy()
    {
        Vector3 lineCastPos = tedTransform.up * height + tedTransform.position - tedTransform.right * width / 6;
        return Physics2D.Linecast(lineCastPos, lineCastPos + tedTransform.right * width * 0.45f * 2, enemyMask);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl player = collision.collider.GetComponent<PlayerControl>();
        string weapon = collision.collider.name;
        Debug.Log("Rocket: "+ weapon);
        if (player != null)
        {
            
            if(isPlayerJumpedOnEnemy())
            {
                hurt();
                player.myRigidbody.velocity = new Vector2(player.myRigidbody.velocity.x, player.jumpForce);
                Debug.Log("Enemy got jump damage by player");
            }
        }

        if(weapon == "Rocket(Clone)")
        {
            hurt();
            Debug.Log("Enemy got damage by rocket");
        }

        if ((weapon == "Player_RightHand"&& PlayerFistSound.isFist)|| (weapon == "Player_LeftHand" && PlayerFistSound.isFist))
        {
            hurt();
        }
    }
}
