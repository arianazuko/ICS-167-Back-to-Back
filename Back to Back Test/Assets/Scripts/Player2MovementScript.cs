using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float spinSpeed = 10f;

    private Rigidbody2D rb;

    private float rotation;

    Vector2 movement;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
        }
    }

    private void FixedUpdate()
    {
        // movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            GameController.instance.health -= 10;
            Debug.Log(GameController.instance.health);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            GameController.instance.health -= 10;
            Debug.Log(GameController.instance.health);
        }
    }
}
