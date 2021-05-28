using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2WallController : MonoBehaviour
{ 
    public GameObject enemy1, enemy2;
    public GameObject nextEnemy1, nextEnemy2, nextEnemy3;
    public GameObject stageWall;

    // Update is called once per frame
    void Update()
    {
        if(enemy1 == null && enemy2 == null)
        {
            Destroy(stageWall);
            nextEnemy1.SetActive(true);
            nextEnemy2.SetActive(true);
            nextEnemy3.SetActive(true);
        }


    }
}
