using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LH_Stress : MonoBehaviour
{
    List<Vector2> pathThroughDungeon;
    int nextIndex = 1; //index of next waypoint
    private Vector2 normDir;
    //normalized vector in the direction of the next waypoint
    LH_Movement playerMovement;
    System.Random rand = new System.Random();

    //AudioManager audio = FindObjectOfType<AudioManager>();

    private float horz, vert;

    //List < Vector2 > yourList = myfunction();
    void Start()
    {
        //FindObjectOfType<AudioManager>().zgStressTest();
        pathThroughDungeon = FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
        //gets list of Vector2's that guide the Player through level
        playerMovement = this.GetComponent<LH_Movement>();
        //playerMovement = FindObjectOfType<LH_Movement>() as LH_Movement;
        //creates an instance of LH_Movment to guide Player through level
        playerMovement.speedCoefficient = 50;
        //Speeds demo up.
    }
    void Update()
    {
    }

    void FixedUpdate()
    {
        horz=rand.Next(-10,10);
        vert=rand.Next(-10,10);

        normDir.Set(horz,vert); 
        normDir.Normalize();
        playerMovement.movePlayer(normDir);
    }
}
