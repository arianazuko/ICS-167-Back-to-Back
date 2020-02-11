using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberScript : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5.0f;
    Vector2 targetVector;

    GameObject[] possibleTargets;
    GameObject target;

    public float expRadius = 5.0f;
    public float expPower = 500.0f;
    private float health = 10f;

    private float maxHealth = 10f;
    Transform bar;

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
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == target.name)
        {
            rb.velocity = Vector2.zero;

            target = null;
            this.GetComponentInChildren<SpriteRenderer>().color = new Color(255f, 0f, 0f);

            Transform temp = this.transform.Find("circle");
            temp.gameObject.SetActive(true);
            
            Invoke("explode", 2);
        }
    }


    private void explode()
    {
        //GameObject insObj = (GameObject)Instantiate(explosionEffect);
        SFXManagerScript.instance.PlaySFX(5,0.5f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, expRadius);
        for (int i = 0; i <= colliders.Length - 1; i++)
        {
            Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 directionVector = (rb.transform.position - transform.position).normalized;

                rb.AddForce(directionVector * expPower);
                //rb.velocity = (targetVector * 10f);

                //add line for dealing damage
            }
            if (colliders[i].gameObject.name == "Player 1" || colliders[i].gameObject.name == "Player 2")
            {
                GameController.instance.health -= 10;
                Debug.Log(GameController.instance.health);
            }
        }

        //Destroy(insObj, 2f);
        Transform temp = this.transform.Find("conc");
        temp.gameObject.SetActive(true);

        GameController.instance.numEnemies -= 1;
        Destroy(gameObject,0.1f);

    }

    private void move()
    {
        if (target != null)
        {
            targetVector = (target.transform.position - transform.position).normalized;
            rb.SetRotation(Vector2.Angle(transform.position, targetVector));

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
        if (GameController.instance.specialActivated)
        {
            if (collision.gameObject.name == "Player 2 Energy")
            {
                health -= 2 * Time.fixedDeltaTime;
            }
        }
    }

    void changeHPBar()
    {
        float ratio = health / maxHealth;

        bar.localScale = new Vector3(ratio, bar.localScale.y, bar.localScale.z);

    }

}
