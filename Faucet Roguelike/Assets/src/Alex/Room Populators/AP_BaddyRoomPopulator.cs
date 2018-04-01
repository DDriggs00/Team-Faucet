using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_BaddyRoomPopulator : AP_RoomPopulator {
	
	public override void PopulateRoom (int level)
	{
		//Reset our list of gridpositions.
		InitializeList ();
		// Create a random number of inert obstacles, calling Devin's method
		PopulateRocks(1, 3);
		// Create a random number of interactable items, calling Devin's method
		PopulateInteractables(1,2, 50);
		// Create a semi-random number of enemies, calling Garrett's method.
		PopulateEnemies(4,7);

		Debug.Log ("BADDY ROOM");
	}
}
