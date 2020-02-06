using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float spinSpeed = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector4 originalColor;

    private float rotation;

    Vector2 movement;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIScript.instance.isPaused)
        {
            //Input
            movement.x = Input.GetAxisRaw("Horizontal1");

            movement.y = Input.GetAxisRaw("Vertical1");

            //this will rotate continuously
            if (Input.GetAxisRaw("Rotate") != 0) 
            {
                float spinSpeed = 5f;
                
                this.transform.Rotate(0, 0, spinSpeed * Input.GetAxisRaw("Rotate"));
            }

            //special button
            //only needed for this player since accessing gamecontroller
            if (Input.GetButtonDown("Special") )
            {
                Debug.Log("Special");
                if (GameController.instance.specialMeter >= GameController.instance.maxSpecialMeter)
                {
                    Debug.Log("Special Activated");
                    GameController.instance.specialActivated = true;
                    spriteRenderer.color = Color.gray;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //special logic
        //only needed for this player since accessing gamecontroller
        if (GameController.instance.specialActivated)
        {
            GameController.instance.specialMeter -= GameController.instance.specialDrain * Time.fixedDeltaTime;
            Debug.Log(GameController.instance.specialMeter);
            if(GameController.instance.specialMeter <= 0)
            {
                GameController.instance.specialActivated = false;
                spriteRenderer.color = originalColor;
                GameController.instance.specialMeter = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            if (!GameController.instance.specialActivated)
            {
                GameController.instance.health -= 10;
            }
            Debug.Log(GameController.instance.health);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (!GameController.instance.specialActivated)
            {
                GameController.instance.health -= 10;
            }
            Debug.Log(GameController.instance.health);
        }
    }
}
