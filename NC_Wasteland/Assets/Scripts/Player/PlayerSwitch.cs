using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    private string selecteditem;
    public static List<string> SelectedItems = new List<string>();

    public GameObject myFist;
    public GameObject myRpg;
    public static Vector2 myPosition;
    public Vector2 pos;
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
        pos = myPosition;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameObject.Find("PlayerRpg") || GameObject.Find("PlayerRpg(Clone)"))
            {
                selecteditem = SelectedItems[0];
                Destroy(GameObject.Find("PlayerRpg"));
                Destroy(GameObject.Find("PlayerRpg(Clone)"));
                Instantiate(myFist, myPosition, Quaternion.identity);
                PlayerControl.facingRight = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameObject.Find("PlayerFist") || GameObject.Find("PlayerFist(Clone)"))
            {
                selecteditem = SelectedItems[1];
                Destroy(GameObject.Find("PlayerFist"));
                Destroy(GameObject.Find("PlayerFist(Clone)"));
                Instantiate(myRpg, myPosition, Quaternion.identity);
                PlayerControl.facingRight = true;
            }
        }
    }
}
