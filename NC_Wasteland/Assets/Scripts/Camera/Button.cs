using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void playAgain()
    {
        Globals.PlayerisDeath = false;
        Globals.Player_Health = 100;
        SceneManager.LoadScene("TestScene");
    }

    public void exit()
    {
        Application.Quit();
    }
}
