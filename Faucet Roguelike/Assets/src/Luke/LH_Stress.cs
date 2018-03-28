using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LH_Stress : MonoBehaviour
{
    List<Vector2> mPathThroughDungeon;
    private Vector2 mRandomDir;
    LH_Movement mPlayerMovment;
    System.Random mRandNum = new System.Random();
    private float mHorz, mVert;
    void Start()
    {
        mPathThroughDungeon = FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
        //gets list of Vector2's that guide the Player through level
        mPlayerMovment = this.GetComponent<LH_Movement>();
        mPlayerMovment.mSpeedCoefficient = 50; //speeds demo up

    }
    void Update()
    {
    }
    void FixedUpdate()
    {
        mHorz=mRandNum.Next(-10,10);
        mVert=mRandNum.Next(-10,10);

        mRandomDir.Set(mHorz,mVert); 
        mPlayerMovment.movePlayer(mRandomDir);
    }
}
