using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    private string selecteditem;
    public static List<string> SelectedItems = new List<string>();
    public static bool isFist=true;

    public GameObject myFist;
    public GameObject myRpg;
    public static Vector2 myPosition;
    public Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        selecteditem = "fist";
        SelectedItems.Add("fist");
        SelectedItems.Add("rpg");//für mehrere items
        myPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameObject.Find("PlayerRpg") || GameObject.Find("PlayerRpg(Clone)"))
            {
                GameObject player;
                selecteditem = SelectedItems[0];

                if (GameObject.Find("PlayerRpg"))
                {
                    player = GameObject.Find("PlayerRpg");
                }
                else
                {
                    player = GameObject.Find("PlayerRpg(Clone)");
                }

                myRigidbody = player.GetComponent<Rigidbody2D>();
                myRigidbody.velocity = player.GetComponent<Rigidbody2D>().velocity;

                Destroy(player);

                GameObject newPlayer = Instantiate(myFist, myPosition, Quaternion.identity);
                newPlayer.GetComponent<Rigidbody2D>().velocity=myRigidbody.velocity;

                if (!PlayerControl.facingRight)
                {
                    newPlayer.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            isFist = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameObject.Find("PlayerFist") || GameObject.Find("PlayerFist(Clone)"))
            {
                GameObject player;
                selecteditem = SelectedItems[1];

                if (GameObject.Find("PlayerFist"))
                {
                    player = GameObject.Find("PlayerFist");
                }
                else
                {
                    player = GameObject.Find("PlayerFist(Clone)");
                }

                myRigidbody = player.GetComponent<Rigidbody2D>();
                myRigidbody.velocity = player.GetComponent<Rigidbody2D>().velocity;

                Destroy(player);

                GameObject newPlayer = Instantiate(myRpg, myPosition, Quaternion.identity);
                newPlayer.GetComponent<Rigidbody2D>().velocity = myRigidbody.velocity;

                if (!PlayerControl.facingRight)
                {
                    newPlayer.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            isFist = false;
        }
    }
}
