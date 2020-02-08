using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public int health = 100;
    int maxHealth = 100;
    public int numHits = 5;
    public int numBullets = 5;
    public float specialMeter = 0;
    public bool specialActivated = false;

    public int maxHits = 3;
    public int maxBullets = 3;
    public float maxSpecialMeter = 100;
    public float specialDrain = 15;

    public int numEnemies;
    private float timer = 0.0f;
    private float maxTime = 8.0f;
    private bool textActive = false;

    public bool oneonecomplete = false;
    public bool onetwocomplete = false;
    public bool twoonecomplete = false;
    public bool threeonecomplete = false;
    public bool twotwocomplete = false;

    public GameObject[] spawners;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timer = 0.0f;
        Debug.Log("loaded");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "1-1":
                if (oneonecomplete == true)
                {
                    foreach (GameObject obj in spawners)
                    {
                        obj.GetComponent<SpawnerScript>().maxMonsterNum = 0;
                    }
                }
                break;
            case "1-2":
                if (onetwocomplete == true)
                {
                    foreach (GameObject obj in spawners)
                    {
                        obj.GetComponent<SpawnerScript>().maxMonsterNum = 0;
                    }
                }
                break;
            case "2-1":
                if (twoonecomplete == true)
                {
                    foreach (GameObject obj in spawners)
                    {
                        obj.GetComponent<SpawnerScript>().maxMonsterNum = 0;
                    }
                }
                break;
            case "2-2":
                if (twotwocomplete == true)
                {
                    foreach (GameObject obj in spawners)
                    {
                        obj.GetComponent<SpawnerScript>().maxMonsterNum = 0;
                    }
                }
                break;
            case "3-1":
                if (threeonecomplete == true)
                {
                    foreach (GameObject obj in spawners)
                    {
                        obj.GetComponent<SpawnerScript>().maxMonsterNum = 0;
                    }
                }
                break;
            default:
                break;
        }
        CountEnemies();
    }

    void CountEnemies()
    {
        foreach(GameObject obj in spawners)
        {
            numEnemies += obj.GetComponent<SpawnerScript>().maxMonsterNum;
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            UIScript.instance.GameOver();
            health = maxHealth;
        }
        if (numEnemies == 0)
        {
            if (textActive == false)
            {
                UIScript.instance.GetToPortal();
                textActive = true;
            }
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        if (timer >= maxTime)
        {
            UIScript.instance.GetToPortal();
            textActive = false;
            Debug.Log("cats");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
