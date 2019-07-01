using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayer;
    public static bool facingRight;
    public Rigidbody2D myRigidbody;

    public AudioSource audiosource;
    public AudioClip jumpClip;
    public AudioClip fallClip;
    public AudioClip hurtClip;

    private bool grounded;
    private Collider2D myCollider;
    private Animator myAnimator;
    private int randomFist;

    private float hitTime = 0.5f;
    private bool hit = false;
    private float stunTime = 0;
    private bool stun = false;
    private Enemy enemy;
    private bool playonce=false;
    private bool airmove = false;
    float x;


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
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, groundLayer);
        PlayerSwitch.myPosition = new Vector2(transform.position.x, transform.position.y);

        if (!grounded && myRigidbody.velocity.x > 0 && Input.GetKey(KeyCode.A) && !stun)
        {
            myRigidbody.velocity = new Vector2(-moveSpeed / 1.5f, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
            airmove = true;
        }
        else if (!grounded && myRigidbody.velocity.x < 0 && Input.GetKey(KeyCode.D) && !stun)
        {
            myRigidbody.velocity = new Vector2(moveSpeed / 1.5f, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
            airmove = true;
        }
        else if (!grounded&&airmove == true&& Input.GetKey(KeyCode.A) && !stun)
        {
            myRigidbody.velocity = new Vector2(-moveSpeed / 1.5f, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
        }
        else if (!grounded && airmove == true && Input.GetKey(KeyCode.D) && !stun)
        {
            myRigidbody.velocity = new Vector2(moveSpeed / 1.5f, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
        }
        else 
        {
            x = myRigidbody.velocity.x;
            x=x / 1.05f;
            myRigidbody.velocity = new Vector2(x, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.A)&&!stun&&!airmove)
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
            myAnimator.Play("PlayerAnimation");
        }
        else if (Input.GetKey(KeyCode.D)&&!stun && !airmove)
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
            airmove = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);

                audiosource.clip = jumpClip;
                audiosource.Play();
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

        if (transform.position.y < -15)
        {
            //SceneManager.LoadScene("TestScene");
            

            if (!playonce)
            {
                Globals.PlayerisDeath = true;
                Globals.Player_Health = 0;

                audiosource.clip = fallClip;
                audiosource.Play();
            }
            playonce = true;
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
        enemy = collision.collider.GetComponent<Enemy>();
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
        if (hitTime >= 0.5)
        {
            stun = true;
            Globals.Player_Health -= 25;
            hit = false;
            hitTime = 0;

            audiosource.clip = hurtClip;
            audiosource.Play();

            if (enemy.transform.position.x>transform.position.x)
            {
                myRigidbody.velocity = new Vector2(-1, 3);
            }
            else
            {
                myRigidbody.velocity = new Vector2(1, 3);
            }
        }
 
        if (Globals.Player_Health == 0)
        {
            //SceneManager.LoadScene("TestScene");
            Globals.PlayerisDeath = true;
        }
    }

    private void hurtTime()
    {
        if (hit)
        {
            hitTime += Time.deltaTime;
        }
        if (stun)
        {
            stunTime += Time.deltaTime;
        }

        if (stunTime > 0.5f)
        {
            stunTime=0;
            stun = false;
        }

    }
}