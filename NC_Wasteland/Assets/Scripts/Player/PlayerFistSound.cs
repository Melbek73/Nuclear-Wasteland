using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFistSound : MonoBehaviour
{
    public AudioClip meleeClip1;
    public AudioClip meleeClip2;
    public AudioClip meleeClip3;
    public AudioSource meleeSource;

    public static bool isFist = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void meleeSound1()
    {
        if (PlayerSwitch.isFist == true)
        {
            isFist = true;

            meleeSource.clip = meleeClip1;
            meleeSource.Play();
        }
    }
    private void meleeSound2()
    {
        if (PlayerSwitch.isFist == true)
        {
            isFist = true;

            meleeSource.clip = meleeClip2;
            meleeSource.Play();
        }
    }
    private void meleeSound3()
    {
        if (PlayerSwitch.isFist == true)
        {
            isFist = true;

            meleeSource.clip = meleeClip3;
            meleeSource.Play();
        }
    }
    private void fistEnd()
    {
        isFist = false;
    }
}
