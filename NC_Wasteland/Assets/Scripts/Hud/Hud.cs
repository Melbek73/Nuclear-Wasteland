using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Score
    Score score;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI highscoreText;

    // Player health
    public Image[] playerHealth = new Image[4];
    private Sprite[] hp = new Sprite[2]; // 1 = heart, 0 = no heart

    // Start is called before the first frame update
    void Start()
    {
        // Score
        score = new Score();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        highscoreText = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
        highscoreText.text = "Highscore: " + Globals.Hud_Highscore;

        // PlayerHealth
        for(int i = 0; i < 4; i++)
        {
            GameObject health = GameObject.Find("hp" + (i + 1));
            if(health != null)
            {
                playerHealth[i] = health.GetComponent<Image>();
                Debug.Log("playerHealth got Image");
            } else
            {
                Debug.Log("playerHealth is empty: " + "hp" + (i + 1));
            }
        }

        hp = Resources.LoadAll<Sprite>("Textures/Player/health");
        if(hp[0] == null)
        {
            Debug.Log("hp[0] == null something went wrong");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Score
        score.update(scoreText, highscoreText);

        // Player
        checkPlayerLife();
    }

    // Player
    public void checkPlayerLife()
    {
        if(!Globals.PlayerisDeath)
        {
            switch (Globals.Player_Health)
            {
                case 0:
                    changePlayerHP(0);
                    Debug.Log("changePlayerHP(0) Playerhealth=" + Globals.Player_Health);
                    break;
                case 25:
                    changePlayerHP(1);
                    Debug.Log("changePlayerHP(1) Playerhealth=" + Globals.Player_Health);
                    break;
                case 50:
                    changePlayerHP(2);
                    Debug.Log("changePlayerHP(2) Playerhealth=" + Globals.Player_Health);
                    break;
                case 75:
                    changePlayerHP(3);
                    Debug.Log("changePlayerHP(3) Playerhealth=" + Globals.Player_Health);
                    break;
                case 100:
                    changePlayerHP(4);
                    Debug.Log("changePlayerHP(4) Playerhealth=" + Globals.Player_Health);
                    break;
                default:
                    Debug.Log("Player Health: Something went wrong, players life = " + Globals.Player_Health);
                    break;
            }
        }
    }

    private void changePlayerHP(int heartsLeft)
    {
        if (GameObject.Find("Canvas"))
        {
            for (int i = 4; i > 0; i--)
            {
                playerHealth[i - 1].overrideSprite = (heartsLeft - i >= 0) ? hp[0]
                                                                           : hp[1];
            }
        }
    }
}
