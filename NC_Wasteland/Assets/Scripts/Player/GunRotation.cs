﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Rigidbody2D rocket;              // Prefab of the rocket.
    public float speed = 20f;				// The speed the rocket will fire at.
    public static float bulletAngle = 0.0f;
    public AudioClip rocketClip;
    public AudioSource rocketSource;
    public static bool canShoot;

    private PlayerControl playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;
    private static float rocketTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.root.gameObject.GetComponent<Animator>();
        playerCtrl = transform.root.GetComponent<PlayerControl>();
        rocketSource.clip = rocketClip;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = myPos - target;
        direction.Normalize();
        float bulletAngle;

        if (PlayerControl.facingRight == true)
        {
            bulletAngle = -Vector2.SignedAngle(direction, Vector2.right);
        }
        else
        {
            bulletAngle = -Vector2.SignedAngle(direction, Vector2.left);
        }

        transform.eulerAngles = new Vector3(0, 0, bulletAngle);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                rocketSource.Play();

                bulletAngle = -Vector2.SignedAngle(direction, Vector2.left);
                Rigidbody2D bulletInstance = Instantiate(rocket, myPos, Quaternion.Euler(new Vector3(0, 0, bulletAngle))) as Rigidbody2D;
                bulletInstance.velocity = -direction * speed;

                rocketTime = 0;
            }
        }

        rocketTime += Time.deltaTime;

        if (rocketTime > 2)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }


    }


}
