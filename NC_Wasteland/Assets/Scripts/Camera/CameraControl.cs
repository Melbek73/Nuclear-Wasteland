﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject objectToFollow;
    public float interpolationSpeed = 2.0f;
    public float cameraSpeed = 1.0f;

    // For testing Camera follow the player
    public bool followPlayer = false;

    // Only for Debug
    float nextActionTime = 0.0f;

    public Bounds OrthographicBounds
    {
        get { return CameraExtensions.OrthographicBounds(this.GetComponent<Camera>()); }
    }

    void Start()
    {
        Debug.Log("Camera Bounds Max.X=" + this.OrthographicBounds.max.x);
    }

    void Update()
    {
        float interpolation = interpolationSpeed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y + 0.25f, interpolation);
        position.x = (followPlayer) ? objectToFollow.transform.position.x
                                    : Time.deltaTime * cameraSpeed + position.x;

        this.transform.position = position;

        // Debug
        Globals.DebugAfterTime(ref nextActionTime, 2.0f, "Camera: OrthographicBounds Min.Y=" + OrthographicBounds.min.y + " Min.X=" + OrthographicBounds.min.x + " Max.Y=" + OrthographicBounds.max.y + " Max.X=" + OrthographicBounds.max.x);
    }
}