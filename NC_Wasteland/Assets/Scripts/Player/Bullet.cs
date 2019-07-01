using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    public float destroyDelay;
    public TextMeshProUGUI healthText;
    public bool isplayer;

    private Collider2D myCollider;
    private bool onlyonce=false;
    private float hitTime = 0.5f;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 11);
        Destroy(gameObject, destroyDelay);
        if (Globals.PlayerisDeath == false)
        {
            healthText = GameObject.FindGameObjectWithTag("health").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            hitTime += Time.deltaTime;
        }
        /*if (GameObject.Find("PlayerRpg") || GameObject.Find("PlayerRpg(Clone)"))
        {
            player = GameObject.Find("PlayerRpg(Clone)");
            myCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, this.GetComponent<Collider2D>());
        }
        else if (GameObject.Find("PlayerFist") || GameObject.Find("PlayerFist(Clone)"))
        {
            player = GameObject.Find("PlayerFist(Clone)");
            myCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(myCollider, this.GetComponent<Collider2D>());
        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        explosion.transform.position = transform.position;
        Instantiate(explosion);

        string col = collision.collider.name;

        if (!isplayer&& Globals.PlayerisDeath == false&&(col== "PlayerRpg(Clone)"|| col == "PlayerFist(Clone)"|| col == "PlayerFist"))
        {
            hit = true;
            if (hitTime >= 0.5)
            {
                Globals.Player_Health -= 25;
                //healthText.text = Globals.Player_Health.ToString();
                hit = false;
                hitTime = 0;
                if (Globals.Player_Health <= 0)
                {
                    Globals.PlayerisDeath = true;
                }
            }
        }
    }
}
