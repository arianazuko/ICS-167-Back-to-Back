using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private Collider2D shieldCollider;
    private SpriteRenderer shieldSpriteRenderer;
    private bool shieldMode = true;

    void Start()
    {
        shieldCollider = this.GetComponent<Collider2D>();
        shieldSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(GameController.instance.numHits <= 0)
        {
            shieldCollider.enabled = false;
            shieldSpriteRenderer.enabled = false;
        }
        else
        {
            shieldCollider.enabled = true;
            shieldSpriteRenderer.enabled = true;
        }

        if (Input.GetButtonDown("Shield Switch"))
        {
            shieldMode = !shieldMode;
            if (shieldMode)
            {
                shieldSpriteRenderer.color = Color.white;
            }
            else
            {
                shieldSpriteRenderer.color = Color.red;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        // if (collision.gameObject.tag == "Player")
        // {
        //     Physics2D.IgnoreCollision(collision.collider, shieldCollider);
        // }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (shieldMode)
            {
                if (GameController.instance.numHits > 0)
                {
                    Destroy(collision.gameObject);
                    GameController.instance.numHits -= 1;
                    GameController.instance.specialMeter += 4;
                }
            }
        }
        else if (collision.gameObject.tag == "SpecialEnemyBullet")
        {
            if (shieldMode)
            {
                Destroy(collision.gameObject);
                GameController.instance.numHits -= 1;
            }
            else
            {
                Destroy(collision.gameObject);
                GameController.instance.specialMeter += 5;
            }
        }

    }
}
