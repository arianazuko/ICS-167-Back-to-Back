using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerBehaviour : MonoBehaviour
{
    public GameObject monsterType;
    public int maxMonsterNum;
    int MonsterNum = 0;
    public float spawnDelay;
    bool pause;
    //GameObject[] monsterArray;

    public GameObject targetObj; //fix to auto target players?

    // Start is called before the first frame update
    void Start()
    {
        //monsterArray = new GameObject[maxMonsterNum];
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterNum < maxMonsterNum && !pause)
        {
            MonsterNum++;
            Invoke("spawnMob", spawnDelay);
            pause = true;
            
        }
        
    }

    void spawnMob()
    {
        GameObject insObj = (GameObject)Instantiate(monsterType,transform.position,transform.rotation);
        bomberBehaviourScript objScript = insObj.GetComponent<bomberBehaviourScript>();

        objScript.target = targetObj;
        pause = false;


    }
}
