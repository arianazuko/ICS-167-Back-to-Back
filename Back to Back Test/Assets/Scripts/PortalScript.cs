using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [SerializeField] protected string transportScene;
    [SerializeField] protected SpriteRenderer portalSprite;
    [SerializeField] protected float portalRadius = 1.0f;

    private bool oneinside = false;
    private bool twoinside = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.numEnemies == 0)
        {
            portalSprite.enabled = true;
            checkBothPlayers();
        }
    }

    void checkBothPlayers()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, portalRadius);
        for (int i = 0; i <= colliders.Length - 1; i++)
        {
            if (colliders[i].gameObject.name == "Player 1")
            {
                oneinside = true;
            }
            else if (colliders[i].gameObject.name == "Player 2")
            {
                twoinside = true;
            }
        }
        if (oneinside && twoinside)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            switch (currentScene)
            {
                case "1-1":
                    GameController.instance.oneonecomplete = true;
                    break;
                case "1-2":
                    GameController.instance.onetwocomplete = true;
                    break;
                case "2-1":
                    GameController.instance.twoonecomplete = true;
                    break;
                case "2-2":
                    GameController.instance.twotwocomplete = true;
                    break;
                case "3-1":
                    GameController.instance.threeonecomplete = true;
                    break;
                default:
                    break;
            }
            SceneManager.LoadScene(transportScene);
        }
        else
        {
            oneinside = false;
            twoinside = false;
        }
    }
}
