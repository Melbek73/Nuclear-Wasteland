using TMPro;
using UnityEngine;

public class Score
{
    private static int score;
    private static int highscore;
    private float actionTime;

    public Score()
    {
        score = 0;
        this.actionTime = 0;
    }

    public void update(TextMeshProUGUI scoreText)
    {
        actionTime += Time.deltaTime;
        if(actionTime >= 1)
        {
            // reset
            actionTime = actionTime % 1;
            score += 5;
            Debug.Log("Score = " + score);
        }

        scoreText.text = "score:" + score;
    }

    public static void bonus(int scorepoints)
    {
        score += scorepoints;
    }
}
