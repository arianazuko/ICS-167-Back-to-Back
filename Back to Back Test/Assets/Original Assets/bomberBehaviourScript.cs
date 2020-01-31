using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomberBehaviourScript : MonoBehaviour
{
    Rigidbody2D rib;
    public GameObject target;
    float speed = 5.0f;
    Vector2 targetVector;

    public GameObject explosionEffect;

    public float expRadius = 10.0f;
    public float expPower = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Player");
        rib = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == target.name )
        {
            rib.velocity = Vector2.zero;

            target = null;
            rib.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);

            Invoke("explode", 2);
        }
    }


    private void explode()
    {

        GameObject insObj = (GameObject)Instantiate(explosionEffect,transform.position,transform.rotation);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, expRadius);
        for (int i = 0; i <= colliders.Length - 1; i++)
        {
            Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();
            Vector2 directionVector = (rb.transform.position - transform.position).normalized;

            rb.AddForce(directionVector * expPower);
            print(rb);
            //rb.velocity = (targetVector * 10f);

            //add line for dealing damage
        }

        Destroy(insObj, 2f);
        Destroy(gameObject);

    }

    private void move()
    {
        if (target != null)
        {
            targetVector = (target.transform.position - transform.position).normalized;
            rib.SetRotation(Vector2.Angle(transform.position, targetVector));
            
            rib.velocity = targetVector * speed;            

        }
    }

}
