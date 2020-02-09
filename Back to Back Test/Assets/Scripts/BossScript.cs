using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject[] possibleTargets;
    public GameObject enemyBullet;
    public float bulletSpeed = 15.0f;
    private float health = 300f;

    private int phaseNum = 1;
    private bool runningCoroutine = false;
    private Vector2[] swipeDirections = new Vector2[] { new Vector2(0.20f, 0.20f), new Vector2(-0.20f, -0.20f) };

    [SerializeField] protected GameObject[] spawnableEnemies;
    [SerializeField] protected GameObject[] hopLocations;
    [SerializeField] protected GameObject shield;

    private SpriteRenderer enemySprite;

    void Awake()
    {
        possibleTargets = GameObject.FindGameObjectsWithTag("Player");
        enemySprite = this.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (runningCoroutine == false)
        {
            if (health >= 200)
            {
                phaseNum = 1;
                StartCoroutine(Phase1());
            }
            else if (health >= 100)
            {
                phaseNum = 2;
                StartCoroutine(Phase2());
            }
            else if (health < 100)
            {
                shield.GetComponent<BossShieldScript>().Activate();
                shield.GetComponent<BossShieldScript>().moving = true;
                phaseNum = 3;
                StartCoroutine(Phase3());
            }
        }
    }

    IEnumerator Phase1()
    {
        runningCoroutine = true;
        Vector2 swipe = swipeDirections[Random.Range(0, swipeDirections.Length)];
        Vector2 target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform.position;
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        GameObject projectile1 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        //randomizer for special bullet
        if (Random.value * 100 <= 20)
        {
            projectile1.tag = "SpecialEnemyBullet";
            projectile1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        projectile1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.25f);

        GameObject projectile2 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        if (Random.value * 100 <= 20)
        {
            projectile2.tag = "SpecialEnemyBullet";
            projectile2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        direction += swipe;
        projectile2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.25f);

        GameObject projectile3 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        if (Random.value * 100 <= 20)
        {
            projectile3.tag = "SpecialEnemyBullet";
            projectile3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        direction += swipe;
        projectile3.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.25f);

        GameObject projectile4 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        if (Random.value * 100 <= 20)
        {
            projectile4.tag = "SpecialEnemyBullet";
            projectile4.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        direction += swipe;
        projectile4.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.25f);

        GameObject projectile5 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        if (Random.value * 100 <= 20)
        {
            projectile5.tag = "SpecialEnemyBullet";
            projectile5.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        direction += swipe;
        projectile5.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            
        yield return new WaitForSeconds(3.0f);

        runningCoroutine = false;
    }

    IEnumerator Phase2()
    {
        runningCoroutine = true;
        GameObject chosenEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Length)];

        float xp = Random.Range(0f, 1f);
        float yp = Random.Range(0f, 1f);

        GameObject enemy1 = (GameObject)Instantiate(chosenEnemy, Camera.main.ViewportToWorldPoint(new Vector3(xp, yp, 1)), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        Vector2 target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform.position;
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = target - myPos;
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
        projectile2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.5f);

        GameObject projectile3 = (GameObject)Instantiate(enemyBullet, myPos, Quaternion.identity);
        if (Random.value * 100 <= 20)
        {
            projectile3.tag = "SpecialEnemyBullet";
            projectile3.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        projectile3.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(2.0f);
        runningCoroutine = false;
    }

    IEnumerator Phase3()
    {
        runningCoroutine = true;
        Vector3 hop = hopLocations[Random.Range(0, hopLocations.Length)].transform.position;
        transform.position = hop;

        int[] behavior = new int[] { 1, 2 };
        int chosen = behavior[Random.Range(0, behavior.Length)];
        if (chosen == 1)
        {
            StartCoroutine(Phase1());
        }
        else if (chosen == 2)
        {
            StartCoroutine(Phase2());
        }


        yield return new WaitForSeconds(3.0f);
        runningCoroutine = false;
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
