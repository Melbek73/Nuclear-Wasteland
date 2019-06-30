using TMPro;
using UnityEngine;

public class Score
{
    private static int score;
    private float actionTime;

    public Score()
    {
        score = 0;
        this.actionTime = 0;
    }

    public void update(TextMeshProUGUI scoreText, TextMeshProUGUI highscoreText)
    {
        if(!Globals.PlayerisDeath)
        {
            actionTime += Time.deltaTime;
            if (actionTime >= 1)
            {
                // reset
                actionTime = actionTime % 1;
                score += 5;
                Debug.Log("Score = " + score);

                if (Globals.Hud_Highscore < score)
                {
                    Globals.Hud_Highscore = score;
                    highscoreText.text = "highscore: " + Globals.Hud_Highscore;
                }
            }

            scoreText.text = "score: " + score;
        }
    }

    public static string lastScore()
    {
        return score.ToString();
    }

    public static void bonus(int scorepoints)
    {
        score += scorepoints;
    }
}
