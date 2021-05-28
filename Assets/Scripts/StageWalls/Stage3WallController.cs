using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3WallController : MonoBehaviour
{
    public GameObject enemy1, enemy2, enemy3;
    public GameObject nextEnemy;
    public GameObject stageWall;

    // Update is called once per frame
    void Update()
    {
        if(enemy1 == null && enemy2 == null && enemy3 == null)
        {
            Destroy(stageWall);
            nextEnemy.SetActive(true);
        }


    }
}
