using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_BigRoomPopulator : AP_RoomPopulator {

	bool handleBoss = false;
	protected Vector3 offset = Vector2.zero;							// offset of pouplating objects to match big room

	public override void Setup(AP_Room r, DD_GenObstacle g)
	{
		obstacleGenerator = g;
		room = r;
		int newSize = 2 * r.GetSize () - 4; // to get inset from sides of new large room
		rows = newSize;//r.GetSize () - 3;
		columns = rows;
		float offsetMod = r.GetSize () / 2f;
		Debug.Log ("OFFSET MOD FOR LARGE ROOM IS " + offsetMod);
		offset = new Vector2 (offsetMod, offsetMod);
		if (FindObjectsOfType<AP_BigRoomPopulator> ().Length == 1)
			handleBoss = true;
	}

	public override void PopulateRoom (int level)
	{
		//Reset our list of gridpositions.
		InitializeList ();
		// Create a random number of inert obstacles, calling Devin's method
		PopulateRocks(15, 18);
		// Create a random number of interactable items, calling Devin's method
		PopulateInteractables(3,7, 35);
		// Create a semi-random number of enemies, calling Garrett's method.
		PopulateEnemies(8,16);

		if(handleBoss)
		{
			// Place a big bad boss enemy somewhere in the room
		}
	}

	protected override void Set(GameObject thing)
	{
		Vector3 randomPosition = RandomPosition ();
		thing.transform.parent = room.transform;
		thing.transform.localPosition = randomPosition + offset;
	}
}
