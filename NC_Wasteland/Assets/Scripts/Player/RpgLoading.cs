using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgLoading : MonoBehaviour
{
    public Sprite emptyRpg;
    public Sprite rpg;

    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!GunRotation.canShoot)
        {
            mySpriteRenderer.sprite = emptyRpg;
        }
        else
        {
            mySpriteRenderer.sprite = rpg;
        }
    }
}
