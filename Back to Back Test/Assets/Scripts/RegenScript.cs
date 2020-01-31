using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenScript : MonoBehaviour
{
    public float passedTime = 0f;
    public float neededTime = 0.75f;

    public Color regenColor;
    public Color myColor;
    public Color otherColor;

   void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 2 Energy")
        {
            this.GetComponent<SpriteRenderer>().color = regenColor;
            other.gameObject.GetComponent<SpriteRenderer>().color = regenColor;
            regenColor.a = 0.42f;
            passedTime += Time.deltaTime;
            if (passedTime >= neededTime)
            {
                if (GameController.instance.numBullets < GameController.instance.maxBullets)
                    GameController.instance.numBullets += 1;
                if (GameController.instance.numHits < GameController.instance.maxHits)
                    GameController.instance.numHits += 1;
                passedTime = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 2 Energy")
        {
            passedTime = 0;
            this.GetComponent<SpriteRenderer>().color = myColor;
            other.gameObject.GetComponent<SpriteRenderer>().color = otherColor;
            myColor.a = 0.42f;
            otherColor.a = 0.42f;
        }
    }
}
