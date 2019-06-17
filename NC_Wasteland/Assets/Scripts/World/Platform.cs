using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform
{
    public float height;
    public float width;
    private Bounds boundaries;
    public GameObject prefabPlatform;
    
    public Platform(GameObject prefabPlatform) {
        //this.x = prefabPlatform.GetComponentInChildren<Transform>().Find("pointRight").position.x;
        this.height = prefabPlatform.GetComponent<BoxCollider2D>().size.y;
        this.width = prefabPlatform.GetComponent<SpriteRenderer>().size.x;
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

    public Bounds Boundaries
    {
        get
        {
            Bounds ret = this.prefabPlatform.GetComponent<BoxCollider2D>().bounds;
            float minX = prefabPlatform.GetComponentInChildren<Transform>().Find("minX").position.x;
            float minY = prefabPlatform.GetComponentInChildren<Transform>().Find("minY").position.y;
            float maxX = prefabPlatform.GetComponentInChildren<Transform>().Find("maxX").position.x;
            float maxY = prefabPlatform.GetComponentInChildren<Transform>().Find("maxY").position.y;
            ret.SetMinMax(new Vector2(minX, minY), new Vector2(maxX, maxY));

            return ret;
        }
    }
}
