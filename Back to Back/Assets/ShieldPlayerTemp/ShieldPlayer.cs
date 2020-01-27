using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlayer : MonoBehaviour
{
    //movement vector
    private Vector2 playerMovement;
    //setup for shieldplayer
    private Rigidbody2D rb;
    private Transform t;
    private Animator anim;
    public float moveSpeed = 3f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        playerMovement = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
        
        //need idle states? or animator?
        //anim.SetFloat("X", playerMovement.x);
        //anim.SetFloat("Y", playerMovement.y);

        //shield rotation
        if (Input.GetButtonDown("Rotate")) {
            //rotating entire gameObject
            t.Rotate(0, 0, 45 * Input.GetAxisRaw("Rotate"));
        }
    }
    void FixedUpdate()
    {
        //for player movement
        rb.MovePosition(rb.position + playerMovement * moveSpeed * Time.fixedDeltaTime);
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        //putting placeholder function until projectile is done
        if (collide.gameObject.tag == "enemyProjectile") {
            Destroy(collide.gameObject);
            //may need to add something for blocking bullets charging something
        }
        if (collide.gameObject.tag == "enemy") {
            //take damage
        }
    }
}