using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Rigidbody2D shot;

    GameObject player;

    private float shootDelay=0;
    private float bulletAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("PlayerRpg(Clone)"))
        {
            player = GameObject.Find("PlayerRpg(Clone)");
        }
        else if (GameObject.Find("PlayerRpg"))
        {
            player = GameObject.Find("PlayerRpg");
        }
        else if (GameObject.Find("PlayerFist(Clone)"))
        {
            player = GameObject.Find("PlayerFist(Clone)");
        }
        else if (GameObject.Find("PlayerFist"))
        {
            player = GameObject.Find("PlayerFist");
        }

        shootDelay += Time.deltaTime;

        if (shootDelay > 3)
        {
            shootDelay = 0;

            Vector2 direction = transform.position - player.transform.position;
            direction.Normalize();

            bulletAngle = -Vector2.SignedAngle(direction, Vector2.right);

            Rigidbody2D bulletInstance = Instantiate(shot, transform.position, Quaternion.Euler(new Vector3(0, 0, bulletAngle))) as Rigidbody2D;
            bulletInstance.velocity = -direction * 3;
        }
    }
}
