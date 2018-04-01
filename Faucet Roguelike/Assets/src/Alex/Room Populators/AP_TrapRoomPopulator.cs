using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_TrapRoomPopulator : AP_RoomPopulator 
{

	public override void PopulateRoom (int level)
	{
		//Reset our list of gridpositions.
		InitializeList ();
		// Create a random number of inert obstacles, calling Devin's method
		PopulateRocks(2, 5);
		// Create a random number of interactable items, calling Devin's method
		PopulateInteractables(5,12,0);
		// Create a semi-random number of enemies, calling Garrett's method.
		PopulateEnemies(1,3);
		Debug.Log ("TRAP ROOM!!!");
	}
}
