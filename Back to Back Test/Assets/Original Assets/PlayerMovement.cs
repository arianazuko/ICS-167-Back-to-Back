using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    Object bulletRef; 

    Vector2 movement;

    private void Start()
    {
        bulletRef = Resources.Load("GreenCircle");
    }

    // Update is called once per frame
    void Update()
    {

        //Input
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.transform.position = rb.position;
        }

    }

    private void FixedUpdate()
    {   
        // movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
