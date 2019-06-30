using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public int health;
    public LayerMask groundLayer;
    public static bool facingRight;
    public Rigidbody2D myRigidbody;
    public TextMeshProUGUI healthText;

    private bool grounded;
    private Collider2D myCollider;
    private Animator myAnimator;
    private int randomFist;

    private float hitTime = 1;
    private bool hit = false;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator= GetComponent<Animator>();
        //PlayerSwitch.myPosition=new Vector2(transform.position.x, transform.position.y);

        // load globals
        this.jumpForce = Globals.Player_JumpForce;
        this.moveSpeed = Globals.Player_Velocity;
        this.health = Globals.Player_Health;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, groundLayer);
        PlayerSwitch.myPosition = new Vector2(transform.position.x, transform.position.y);

        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
        }

        else
        {
            myAnimator.Play("PlayerStay");
        }

        if (grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

        if (Input.GetKey(KeyCode.Mouse0)&&PlayerSwitch.isFist==true)
        {
            PlayerFistSound.isFist = false;
            randomFist = Random.Range(0, 3);
            if (randomFist == 0)
            {
                myAnimator.Play("FistAnimation");
            }
            else if (randomFist == 1)
            {
                myAnimator.Play("FistAnimation2");
            }
            else if (randomFist == 2)
            {
                myAnimator.Play("FistAnimation3");
            }
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePosition.x < transform.position.x && !facingRight)
        {
            Flip();
        }

        if (transform.position.y < -20)
        {
            SceneManager.LoadScene("TestScene");
        }

        hurtTime();
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            if(enemy.isPlayerJumpedOnEnemy())
            {
                Debug.Log("Player jumped on enemy");
            } else
            {
                hurt();
            }
        }
    }

    private void hurt()
    {
        hit = true;
        if (hitTime >= 1)
        {
            health -= 30;
            healthText.text =health.ToString();
            hit = false;
            hitTime = 0;
        }
 
        if (health < 0)
        {
            SceneManager.LoadScene("TestScene");
        }
    }

    private void hurtTime()
    {
        if (hit)
        {
            hitTime += Time.deltaTime;
        }
    }
}