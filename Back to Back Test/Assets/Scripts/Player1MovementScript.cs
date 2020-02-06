using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb;
    public GameObject bullet;

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
            movement.x = Input.GetAxisRaw("Horizontal");

            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
                if (GameController.instance.numBullets > 0)
                {
                    Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
                    Vector2 direction = target - myPos;
                    direction.Normalize();
                    GameObject projectile = (GameObject)Instantiate(bullet, myPos, Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                    //special makes player rapid fire with unlimited ammo
                    if (!GameController.instance.specialActivated)
                    {
                        GameController.instance.numBullets -= 1;
                    }
                }
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
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "SpecialEnemyBullet")
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
