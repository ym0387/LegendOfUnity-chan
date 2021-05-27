using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1WallController : MonoBehaviour
{ 
    public GameObject enemy1;
    public GameObject stageWall;

    public GameObject nextEnemy1, nextEnemy2;

    // Update is called once per frame
    void Update()
    {
        if(enemy1 == null)
        {
            Destroy(stageWall);
            nextEnemy1.SetActive(true); 
            nextEnemy2.SetActive(true);

        }


    }
}
