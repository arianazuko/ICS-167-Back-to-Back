using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private Collider2D shieldCollider;
    private SpriteRenderer shieldSpriteRenderer;
    //for if they are blocking regular or special bullets
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
            //just to identify which shield is on
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
        //added logic for special bullets
        //if shield mode, blocks regular and special but gives normal amount of special
        //if special block, does blocks regular but takes extra damage and special gains extra special
        //special does not deal damage if blocking with special
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
         {
             Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), shieldCollider);
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (shieldMode)
            {
                if (GameController.instance.numHits > 0)
                {
                    Destroy(collision.gameObject);
                    if (!GameController.instance.specialActivated)
                    {
                        GameController.instance.numHits -= 1;
                        GameController.instance.specialMeter += 4;
                    }
                }
            }
            else
            {
                GameController.instance.numHits -= 2;
            }
        }
        else if (collision.gameObject.tag == "SpecialEnemyBullet")
        {
            if (shieldMode)
            {
                Destroy(collision.gameObject);
                if (!GameController.instance.specialActivated)
                {
                    GameController.instance.numHits -= 1;
                }
            }
            else
            {
                Destroy(collision.gameObject);
                if (!GameController.instance.specialActivated)
                {
                    GameController.instance.specialMeter += 8;
                }
            }
        }

    }
}
