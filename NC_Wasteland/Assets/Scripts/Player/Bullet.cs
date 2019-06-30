﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    public float destroyDelay;
    private Collider2D myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 11);
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {

        /*if (GameObject.Find("PlayerRpg") || GameObject.Find("PlayerRpg(Clone)"))
        {
            player = GameObject.Find("PlayerRpg(Clone)");
            myCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, this.GetComponent<Collider2D>());
        }
        else if (GameObject.Find("PlayerFist") || GameObject.Find("PlayerFist(Clone)"))
        {
            player = GameObject.Find("PlayerFist(Clone)");
            myCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, this.GetComponent<Collider2D>());
        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        explosion.transform.position = transform.position;
        Instantiate(explosion);
    }
}
