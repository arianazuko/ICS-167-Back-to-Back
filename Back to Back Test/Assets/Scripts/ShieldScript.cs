using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private Collider2D shieldCollider;
    private SpriteRenderer shieldSpriteRenderer;

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, shieldCollider);
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (GameController.instance.numHits > 0)
            {
                Destroy(collision.gameObject);
                GameController.instance.numHits -= 1;
            }
        }

    }
}
