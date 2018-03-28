using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_AutoRun : MonoBehaviour 
{
	List<Vector2> mPathThroughDungeon;
	int mNextIndex = 1 ; //index of next waypoint
    private Vector2 mWaypointDir;
	private float mDist;
	private float mMinDist = 0.25F;  
    LH_Movement mPlayerMovment;
	
    void Start () {
		mPathThroughDungeon=FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
		//gets list of Vector2's that guide the Player through level
        mPlayerMovment = this.GetComponent<LH_Movement>();
        mPlayerMovment.mSpeedCoefficient=5;
	}
	void Update () {
	}

	void FixedUpdate()
	{
		mDist=Vector2.Distance(mPlayerMovment.mRigidPlayer.position,mPathThroughDungeon[mNextIndex]);
		if(mDist<mMinDist)
		{
			mNextIndex++;
		}
	
		mWaypointDir = (mPathThroughDungeon[mNextIndex]-mPlayerMovment.mRigidPlayer.position);
        mPlayerMovment.movePlayer(mWaypointDir);
	}
}
