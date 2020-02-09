using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldScript : MonoBehaviour
{
    private Collider2D shieldCollider;
    private SpriteRenderer shieldSpriteRenderer;

    private Vector3 pos1 = new Vector3(-2, -1.5f, -1);
    private Vector3 pos2 = new Vector3(2, -1.5f, -1);

    private int numHits = 6;

    public bool moving = false;

    void Start()
    {
        shieldCollider = this.GetComponent<Collider2D>();
        shieldSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (numHits <= 0)
        {
            shieldCollider.enabled = false;
            shieldSpriteRenderer.enabled = false;
        }
        else
        {
            shieldCollider.enabled = true;
            shieldSpriteRenderer.enabled = true;
        }
        if (moving == true)
        {
            transform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * 1.0f, 1.0f));
        }
    }

    public void Activate()
    {
        shieldCollider.enabled = true;
        shieldSpriteRenderer.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //added logic for special bullets
        //if shield mode, blocks regular and special but gives normal amount of special
        //if special block, does blocks regular but takes extra damage and special gains extra special
        //special does not deal damage if blocking with special
        Debug.Log(collision.gameObject.tag);
        Debug.Log(numHits);
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), shieldCollider);
        }
        if (collision.gameObject.tag == "PlayerBullet")
        {
            if (numHits > 0)
            {
                Destroy(collision.gameObject);
                numHits -= 1;
            }
        }
            
    }
}
