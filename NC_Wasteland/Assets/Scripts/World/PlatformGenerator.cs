using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject lastPlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    private GameObject prefabPlatform;

    // Start is called before the first frame update
    void Start()
    {
        prefabPlatform = (GameObject)Resources.Load("Prefabs/Worldgeneration/Level1/Level1_0", typeof(GameObject));
        platformWidth = lastPlatform.GetComponent<BoxCollider2D>().size.x;
        //Debug.Log("platform width:" + platformWidth);

        transform.position = new Vector2(transform.position.x + platformWidth + distanceBetween, lastPlatform.transform.position.y);
        Instantiate(lastPlatform, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector2(transform.position.x + platformWidth + distanceBetween, lastPlatform.transform.position.y);
            lastPlatform.transform.position = transform.position;
            Instantiate(prefabPlatform, transform.position, transform.rotation);
        }
    }
}