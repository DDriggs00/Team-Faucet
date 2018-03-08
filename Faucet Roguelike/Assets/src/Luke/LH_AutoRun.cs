using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_AutoRun : MonoBehaviour 
{
	List<Vector2> pathThroughDungeon;
	int nextIndex = 1 ; //index of next waypoint
    private Vector2 waypointDir;
	private float dist;
	private float minDist = 0.25F;  
    LH_Movement playerMovement;
	
    void Start () {
		pathThroughDungeon=FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
		//gets list of Vector2's that guide the Player through level
        playerMovement = this.GetComponent<LH_Movement>();
        playerMovement.speedCoefficient=5;
	}
	void Update () {
	}

	void FixedUpdate()
	{
		dist=Vector2.Distance(playerMovement.rigidPlayer.position,pathThroughDungeon[nextIndex]);
		if(dist<minDist)
		{
			nextIndex++;
		}
	
		waypointDir = (pathThroughDungeon[nextIndex]-playerMovement.rigidPlayer.position);
        playerMovement.movePlayer(waypointDir);
	}
}
