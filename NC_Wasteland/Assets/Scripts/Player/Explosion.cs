﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.1f);
        AudioSource explosion = GetComponent<AudioSource>();
        explosion.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}