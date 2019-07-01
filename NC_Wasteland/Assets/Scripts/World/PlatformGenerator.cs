using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject startPlatform;
    public Camera mainCamera;

    private float platformWidth;
    private Platform activePlatform;
    private Queue<GameObject> platformQueue = new Queue<GameObject>(); // for deleting not visible platforms
    private float y_line;
    private int amountOfPlatforms;

    // Enemy
    GameObject enemyTed;
    GameObject enemyDogl;
    private GameObject enemyRandom;
    private int enemyRandomint;
    private float percentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        amountOfPlatforms = getAmountOfPlatforms();     // amount of platforms per level
        activePlatform = new Platform(startPlatform);   // load startPlatform
        y_line = Globals.Platform_YAxis;                // y axis for platforms

        enemyTed = (GameObject)Resources.Load("Prefabs/Enemy/Ted", typeof(GameObject));
        enemyDogl = (GameObject)Resources.Load("Prefabs/Enemy/Dogl", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < mainCamera.OrthographicBounds().max.x)
        {
            Platform nextPlatform = initNextPlatform();

            // position of generation
            transform.position = platformPosition(activePlatform, nextPlatform);
            nextPlatform.Position = transform.position;

            // set nextPlatform to activePlatform
            activePlatform = nextPlatform;

            // init platform and add it to queue
            platformQueue.Enqueue(Instantiate(nextPlatform.prefabPlatform, transform.position, nextPlatform.Rotation));

            enemyRandomint = Random.Range(0, 2);
            if (enemyRandomint == 1)
            {
                enemyRandom = enemyTed;
            }
            else
            {
                enemyRandom = enemyDogl;
            }

            // Enemy on platform
            float enemySpawnProbability = 35.0f; // percent of enemy spawn
            if((int)Score.lastScore > 500)
            {
                enemySpawnProbability = 65.0f;
            } else if((int)Score.lastScore > 1000)
            {
                enemySpawnProbability = 100.0f;
            }

            if(Random.Range(0, 100) < enemySpawnProbability)
            {
                Instantiate(enemyRandom, transform.position + Vector3.up + Vector3.back, enemyRandom.transform.rotation);
            }
        }

        // Destroy platforms
        if (platformQueue.Count > 15)
        {
            Destroy(platformQueue.Dequeue());
        }
    }

    private Vector2 platformPosition(Platform oldPlatform, Platform newPlatform)
    {
        float x = 0.0f;
        float y = 0.0f;
        float holeProbability = 12.5f; // in percent

        // max y distance between old and new platform
        y = y_line + newPlatform.height + Random.Range(0.0f, 4.0f);
        while (y - oldPlatform.Boundaries.min.y + oldPlatform.height > Globals.Player_JumpForce - 2.0f)
        {
            y -= Random.Range(0.5f, 2.0f);
        } 

        // max x distance between old and new platform
        x = oldPlatform.Position.x + newPlatform.width + Random.Range(-0.5f, -0.2f);
        if(Random.Range(0, 100) < holeProbability)
        {
            x += Random.Range(0, Globals.Player_Velocity);
        }

        return new Vector2(x, y);
    }

    private int getAmountOfPlatforms()
    {
        Object[] maps = Resources.LoadAll("Prefabs/Worldgeneration/Level1/");
        int ret = maps.Length;
        Resources.UnloadUnusedAssets();
        return ret;
    }

    private Platform initNextPlatform()
    {
        string path = "Prefabs/Worldgeneration/Level1/Level1_" + Random.Range(0, amountOfPlatforms);
        return new Platform((GameObject)Resources.Load(path, typeof(GameObject)));
    }
}