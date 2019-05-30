using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayer;
    public static bool facingRight;

    private Rigidbody2D myRigidbody;
    private bool grounded;
    private Collider2D myCollider;
    private Animator myAnimator;

    private int randomFist;



    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator= GetComponent<Animator>();
        //PlayerSwitch.myPosition=new Vector2(transform.position.x, transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, groundLayer);
        PlayerSwitch.myPosition = new Vector2(transform.position.x, transform.position.y);

        if (grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

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
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
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
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}