using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public int health = 100;
    public int numHits = 5;
    public int numBullets = 5;

    public int maxHits = 10;
    public int maxBullets = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        if (health <= 0)
        {
            UIScript.instance.GameOver();
        }
    }
}
