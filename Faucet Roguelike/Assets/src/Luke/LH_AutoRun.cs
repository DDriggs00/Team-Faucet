using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_AutoRun : MonoBehaviour 
{
	List<Vector2> pathThroughDungeon;
	int nextIndex = 1 ; //index of next waypoint
    private Vector2 normDir;
    //normalized vector in the direction of the next waypoint
	private float dist;
	private float minDist = 0.25F;  
    LH_Movement playerMovement;
	
	//List < Vector2 > yourList = myfunction();
    void Start () {
		pathThroughDungeon=FindObjectOfType<AP_DungeonGenerator>().GetPathThroughDungeon();
		//gets list of Vector2's that guide the Player through level
        playerMovement = FindObjectOfType<LH_Movement>();
        //creates an instance of LH_Movment to guide Player through level
        playerMovement.speedCoefficient=5;
		//Slows demo down a bit.
	}
	void Update () {
	}

	void FixedUpdate()
	{
		dist=Vector2.Distance(playerMovement.rigidPlayer.position,pathThroughDungeon[nextIndex]);
		//Debug.Log("Distance: " + dist);
		if(dist<minDist)
		{
            //Debug.Log("Incrementing Index");
			nextIndex++;
			//return;
		}
	
		normDir = (pathThroughDungeon[nextIndex]-playerMovement.rigidPlayer.position);
        normDir.Normalize();
        playerMovement.movePlayer(normDir);
	}
}
