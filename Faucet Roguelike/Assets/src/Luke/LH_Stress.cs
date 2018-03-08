using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LH_Stress : MonoBehaviour
{
    List<Vector2> pathThroughDungeon;
    private Vector2 randomDir;
    LH_Movement playerMovement;
    System.Random randNum = new System.Random();
    private float horz, vert;
    void Start()
    {
        pathThroughDungeon = FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
        //gets list of Vector2's that guide the Player through level
        playerMovement = this.GetComponent<LH_Movement>();
        playerMovement.speedCoefficient = 50; //speeds demo up

    }
    void Update()
    {
    }
    void FixedUpdate()
    {
        horz=randNum.Next(-10,10);
        vert=randNum.Next(-10,10);

        randomDir.Set(horz,vert); 
        playerMovement.movePlayer(randomDir);
    }
}
