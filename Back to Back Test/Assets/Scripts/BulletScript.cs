using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "PlayerBullet")
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject obj in playerObjects)
            {
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }

        }

        if (this.tag == "EnemyBullet")
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject tile = GameObject.Find("Walls");

            foreach (GameObject obj in playerObjects)
            {
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }
            Physics2D.IgnoreCollision(tile.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }

        if (this.tag == "SpecialEnemyBullet")
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject tile = GameObject.Find("Walls");

            foreach (GameObject obj in playerObjects)
            {
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }
            Physics2D.IgnoreCollision(tile.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
        Invoke("DestroySelf", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
