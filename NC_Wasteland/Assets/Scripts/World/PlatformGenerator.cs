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
    GameObject enemy;

    // Score
    public TextMeshProUGUI scoreText;
    int score;
    float nextActionTime;

    // Start is called before the first frame update
    void Start()
    {
        amountOfPlatforms = getAmountOfPlatforms();     // amount of platforms per level
        activePlatform = new Platform(startPlatform);   // load startPlatform
        y_line = Globals.Platform_YAxis;                // y axis for platforms

        enemy = (GameObject)Resources.Load("Prefabs/Enemy/ted", typeof(GameObject));
        score = 0;
        nextActionTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        Debug.Log("score=" + score);
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

            // Enemy on platform
            float enemySpawnProbability = 25.0f;//25.0f; // percent of enemy spawn
            if(Random.Range(0, 100) < enemySpawnProbability)
            {
                Instantiate(enemy, transform.position + Vector3.up, enemy.transform.rotation);
            }
        }

        // Destroy platforms
        if (platformQueue.Count > 15)
        {
            Destroy(platformQueue.Dequeue());
        }
    }

    public void updateScore()
    {
        nextActionTime += Time.deltaTime;

        // All 2 seconds show camera bounds
        if (nextActionTime >= 1)
        {
            // reset
            nextActionTime = nextActionTime % 1;

            score++;
            scoreText.text = "score:" + score;
            Globals.PlayerScore = score;
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