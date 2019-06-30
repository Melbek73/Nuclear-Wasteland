using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = Globals.PlayerScore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
