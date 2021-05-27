using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3WallController : MonoBehaviour
{ 
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject stageWall;

    // Update is called once per frame
    void Update()
    {
        if(enemy1 == null && enemy2 == null && enemy3 == null)
        {
            Destroy(stageWall);
        }


    }
}
