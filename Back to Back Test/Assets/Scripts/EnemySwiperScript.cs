using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwiperScript : MonoBehaviour
{
    private Vector2[] swipeDirections = new Vector2[] { new Vector2(0.15f, 0.15f), new Vector2(-0.15f, -0.15f) };

    private GameObject[] possibleTargets;
    public GameObject enemyBullet;
    public float bulletSpeed = 15.0f;
    private float health = 20f;

    private SpriteRenderer enemySprite;

    private float maxHealth = 20f;
    Transform bar;

    void Awake()
    {
        possibleTargets = GameObject.FindGameObjectsWithTag("Player");
        enemySprite = this.GetComponentInChildren<SpriteRenderer>();

        bar = this.transform.Find("HP Bar");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hop());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameController.instance.numEnemies -= 1;
            if (GameController.instance.specialMeter < GameController.instance.maxSpecialMeter)
            {
                GameController.instance.specialMeter += 3;
            }
            Destroy(this.gameObject);
        }
        else { changeHPBar(); }
    }

    IEnumerator Hop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            float xp = Random.Range(0f, 1f);
            float yp = Random.Range(0f, 1f);
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(xp, yp, 1));

            Vector2 target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform.position;
            Vector2 swipe = swipeDirections[Random.Range(0, swipeDirections.Length)];
            Debug.Log(target);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            if (direction.x > 0)
            {
                enemySprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                enemySprite.flipX = true;
            }
            direction.Normalize();
            GameObject projectile1 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
            //randomizer for special bullet
            if (Random.value * 100 <= 20)
            {
                projectile1.tag = "SpecialEnemyBullet";
                projectile1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            projectile1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            GameObject projectile2 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
            if (Random.value * 100 <= 20)
            {
                projectile2.tag = "SpecialEnemyBullet";
                projectile2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            direction += swipe;
            projectile2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            GameObject projectile3 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
            if (Random.value * 100 <= 20)
            {
                projectile3.tag = "SpecialEnemyBullet";
                projectile3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            direction += swipe;
            projectile3.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            yield return new WaitForSeconds(3.0f);
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
        float ratio = 2* health / maxHealth;

        bar.localScale = new Vector3(ratio, bar.localScale.y, bar.localScale.z);

    }
}
