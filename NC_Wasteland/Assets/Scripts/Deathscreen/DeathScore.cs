using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathScoreText;
    private TextMeshProUGUI deathHighscoreText;
    // Start is called before the first frame update
    void Start()
    {
        deathScoreText = GameObject.Find("DeathScore").GetComponent<TextMeshProUGUI>();
        deathHighscoreText = GameObject.Find("DeathHighscore").GetComponent<TextMeshProUGUI>();


        deathScoreText.text = "Score: " + Score.lastScore();
        deathHighscoreText.text = "Highscore: " + Globals.Hud_Highscore;
        //highscoreText.text = "highscore: " + Globals.Hud_Highscore;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
