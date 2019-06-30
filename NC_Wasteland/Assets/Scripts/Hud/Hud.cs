using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    // Score
    Score score;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Score
        score = new Score();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        highscoreText = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
        highscoreText.text = "Highscore: " + Globals.Hud_Highscore;
    }

    // Update is called once per frame
    void Update()
    {
        // Score
        score.update(scoreText, highscoreText);
    }
}
