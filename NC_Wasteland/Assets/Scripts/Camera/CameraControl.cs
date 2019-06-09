using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject objectToFollow;
    public float interpolationSpeed = 2.0f;
    public float cameraSpeed = 1.0f;

    // For testing Camera follow the player
    public bool followPlayer = false;

    

    void Update()
    {
        float interpolation = interpolationSpeed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y + 0.25f, interpolation);
        position.x = (followPlayer) ? objectToFollow.transform.position.x
                                    : Time.deltaTime * cameraSpeed + position.x;

        this.transform.position = position;
    }
}