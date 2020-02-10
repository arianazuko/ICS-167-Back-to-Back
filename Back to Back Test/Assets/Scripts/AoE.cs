using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : MonoBehaviour
{ 
    Rigidbody2D rb;
    float speed = 5.0f;
    Vector2 targetVector;

    GameObject[] possibleTargets;
    GameObject target;

    public float expRadius = 1.0f;
    public float expPower = 5.0f;
    public int dmg = 5;
    private float health = 10f;

    private float maxHealth = 10f;
    Transform bar;

    float dmgWait = 1.0f;
    bool triggered = false;

// Start is called before the first frame update
void Awake()
{
    possibleTargets = GameObject.FindGameObjectsWithTag("Player");
    target = possibleTargets[Random.Range(0, possibleTargets.Length - 1)];
    rb = GetComponent<Rigidbody2D>();

    bar = this.transform.Find("HP Bar");
}

// Update is called once per frame
void Update()
{
    if (health <= 0)
    {
        GameController.instance.numEnemies -= 1;
        Destroy(this.gameObject);
    }

    else
    {
        changeHPBar();
        move();
            if(triggered)
            {
                delay(dmgWait);
            }
    }
}

private void OnTriggerEnter2D(Collider2D coll)
{
    Debug.Log(coll.gameObject.name);
        if  (!triggered)
        {

            if (coll.gameObject.name == target.name)
            {
                triggered = true;
                rb.velocity = Vector2.zero;

                target = null;

                Transform temp = this.transform.Find("circle");
                temp.gameObject.SetActive(true);
                Transform conTemp = this.transform.Find("conc");
                conTemp.gameObject.SetActive(true);

                //Invoke("explode", 0);
            }
        }
}


private void explode()
{
        delay(dmgWait);
}

private void move()
{
    if (target != null)
    {
            targetVector = (target.transform.position - transform.position).normalized;
   

        rb.velocity = targetVector * speed;

    }
}

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "PlayerBullet")
    {
        Destroy(collision.gameObject);
        health -= 5;
    }

}

void changeHPBar()
{
    float ratio = health / maxHealth;

    bar.localScale = new Vector3(ratio, bar.localScale.y, bar.localScale.z);

}  

IEnumerator delay(float time)
{
    while (true)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, expRadius);

        for (int i = 0; i <= colliders.Length - 1; i++)
        {
            Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();

            if (colliders[i].gameObject.name == "Player 1" || colliders[i].gameObject.name == "Player 2")
            {
                GameController.instance.health -= dmg;
                Debug.Log(GameController.instance.health);
            }
        }
            Debug.Log("tick");
        yield return new WaitForSeconds(time);
    }

}

}
