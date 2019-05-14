using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathmenu : MonoBehaviour
{
    public string mainMenuLevel;

    public void RestartGame()
    {

    }

    public void ReturntoMain()
    {
        Application.LoadLevel(mainMenuLevel);
    }
}
