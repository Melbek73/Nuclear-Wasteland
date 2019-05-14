using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    public string playGameLevel;
    public string Options;

    public void PlayGame()
    {
        Application.LoadLevel(name: playGameLevel);
    }
    public void Optionsscene()
    {
        Application.LoadLevel(name: Options);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
