using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform
{
    public float height;
    public float width;
    public Bounds bounds;
    public GameObject prefabPlatform;
    
    public Platform(GameObject prefabPlatform) {
        //this.x = prefabPlatform.GetComponentInChildren<Transform>().Find("pointRight").position.x;
        this.height = prefabPlatform.GetComponent<BoxCollider2D>().size.y;
        this.width = prefabPlatform.GetComponent<BoxCollider2D>().size.x;
        this.prefabPlatform = prefabPlatform;
    }

    public Vector3 Position
    {
        get { return prefabPlatform.transform.position; }
        set { prefabPlatform.transform.position = value; }
    }

    public Quaternion Rotation
    {
        get { return prefabPlatform.transform.rotation; }
        set { prefabPlatform.transform.rotation = value; }
    }
}
