using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterScript : MonoBehaviour
{
    private GameObject[] possibleTargets;
    public GameObject enemyBullet;
    public float bulletSpeed = 15.0f;
    private float health = 20f;

    void Awake()
    {
        possibleTargets = GameObject.FindGameObjectsWithTag("Player");
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
            Debug.Log(target);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            GameObject projectile1 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
            projectile1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            GameObject projectile2 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
            projectile2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
            GameObject projectile3 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
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
}
