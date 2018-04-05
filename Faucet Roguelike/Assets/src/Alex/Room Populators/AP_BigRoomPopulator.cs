using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_BigRoomPopulator : AP_RoomPopulator {

	bool handleBoss = false;

	public virtual void Setup(AP_Room r, DD_GenObstacle g)
	{
		obstacleGenerator = g;
		room = r;
		rows = r.GetSize () - 3;
		columns = rows;
		offset = r.GetPosition ();
		if (FindObjectsOfType<AP_BigRoomPopulator> ().Length == 1)
			handleBoss = true;
	}

	public override void PopulateRoom (int level)
	{
		//Reset our list of gridpositions.
		InitializeList ();
		// Create a random number of inert obstacles, calling Devin's method
		PopulateRocks(5, 9);
		// Create a random number of interactable items, calling Devin's method
		PopulateInteractables(1,4, 35);
		// Create a semi-random number of enemies, calling Garrett's method.
		PopulateEnemies(2,4);

		if(handleBoss)
		{
			// Place a big bad boss enemy somewhere in the room
		}
	}
}
