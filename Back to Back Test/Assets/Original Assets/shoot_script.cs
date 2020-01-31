using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_script : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float speed = 10f;
    public Vector2 location;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        location = Input.mousePosition;
        location = Camera.main.ScreenToWorldPoint(location);
        Invoke("DestroySelf", 1.5f);
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = location.normalized * speed; 
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
