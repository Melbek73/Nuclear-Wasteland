﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject objectToFollow;
    public float interpolationSpeed = 2.0f;
    public float cameraSpeed = 1.0f;
    public GameObject ui;
    public AudioSource audiosource;
    public AudioClip deathMusicClip;

    // For testing Camera follow the player
    public bool followPlayer = false;

    private bool onlyonce = false;

    // Only for Debug
    float nextActionTime = 0.0f;

    public Bounds OrthographicBounds
    {
        get { return CameraExtensions.OrthographicBounds(this.GetComponent<Camera>()); }
    }

    private void findObjectToFollow()
    {
        if (GameObject.Find("PlayerRpg(Clone)"))
        {
            objectToFollow = GameObject.Find("PlayerRpg(Clone)");
        }
        else if (GameObject.Find("PlayerRpg"))
        {
            objectToFollow = GameObject.Find("PlayerRpg");
        }
        else if (GameObject.Find("PlayerFist(Clone)"))
        {
            objectToFollow = GameObject.Find("PlayerFist(Clone)");
        }
        else if (GameObject.Find("PlayerFist"))
        {
            objectToFollow = GameObject.Find("PlayerFist");
        }
    }

    private void checkPlayerInCamera()
    {
        if (OrthographicBounds.min.x > objectToFollow.transform.position.x)
        {
            Debug.Log("Spieler außerhalb der Kamera");
            Vector2 playerPos = objectToFollow.transform.position;
            playerPos.x = OrthographicBounds.min.x;
            objectToFollow.transform.position = playerPos;
        }
        else if (OrthographicBounds.max.x < objectToFollow.transform.position.x)
        {
            Debug.Log("Spieler außerhalb der Kamera");
            Vector2 playerPos = objectToFollow.transform.position;
            playerPos.x = OrthographicBounds.max.x;
            objectToFollow.transform.position = playerPos;
        }
    }

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        onlyonce = false;
    }

    void Update()
    {
        if (Globals.PlayerisDeath == true)
        {
            if (!onlyonce)
            {

                audiosource.clip = deathMusicClip;
                audiosource.Play();

                Destroy(GameObject.Find("Canvas"));
                Instantiate(ui);
                onlyonce = true;
            }
        } else
        {
            findObjectToFollow();

            float interpolation = interpolationSpeed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y + 0.25f, interpolation);
            position.x = (followPlayer) ? objectToFollow.transform.position.x
                                        : Time.deltaTime * cameraSpeed + position.x;

            this.transform.position = position;

            // Debug
            Globals.DebugAfterTime(ref nextActionTime, 2.0f, "Camera: OrthographicBounds Min.Y=" + OrthographicBounds.min.y + " Min.X=" + OrthographicBounds.min.x + " Max.Y=" + OrthographicBounds.max.y + " Max.X=" + OrthographicBounds.max.x);

            checkPlayerInCamera();
        }
    }
}