using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy attributes
    private int health;
    private bool moveRight = false;

    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        groundLayer.value = 8; // groundlayer has number 8
    }

    // Update is called once per frame
    void Update()
    {
        if()
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != groundLayer)
        {
            moveRight = !moveRight; // toggle direction
        }
    }
}
