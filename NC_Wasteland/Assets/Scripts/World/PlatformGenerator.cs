using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject startPlatform;
    public Transform generationPoint;
    public float jumpForce;

    private float platformWidth;
    private Platform nextPlatform;
    private Platform activePlatform;
    private float y_line;
    private int amountOfPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        amountOfPlatforms = getAmountOfPlatforms();
        activePlatform = new Platform(startPlatform);
        y_line = activePlatform.Position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            initNextPlatform();
            // position of generation
            transform.position = platformPosition(activePlatform, nextPlatform);
            nextPlatform.Position = transform.position;
            // set nextPlatform to activePlatform
            activePlatform = nextPlatform;
            Instantiate(activePlatform.prefabPlatform, transform.position, activePlatform.Rotation);
        }
    }

    private Vector2 platformPosition(Platform oldPlatform, Platform newPlatform)
    {
        float x = 0.0f;
        float y = 0.0f;

        // max y distance between old and new platform
        y = y_line + newPlatform.height + Random.Range(0.0f, 1.0f);
        do
        {
            y -= Random.Range(0.5f, 2.0f);
        } while(y - oldPlatform.Position.y + oldPlatform.height > jumpForce);

        // max x distance between old and new platform
        x = oldPlatform.Position.x + newPlatform.width - 0.5f;
        // coming soon...

        return new Vector2(x, y);
    }

    private int getAmountOfPlatforms()
    {
        Object[] maps = Resources.LoadAll("Prefabs/Worldgeneration/Level1/");
        int ret = maps.Length;
        Resources.UnloadUnusedAssets();
        return ret;
    }

    private void initNextPlatform()
    {
        string path = "Prefabs/Worldgeneration/Level1/Level1_" + Random.Range(0, amountOfPlatforms);
        this.nextPlatform = new Platform((GameObject)Resources.Load(path, typeof(GameObject)));
    }
}