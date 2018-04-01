using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_TreasureRoomPopulator : AP_RoomPopulator {


	public override void PopulateRoom (int level)
	{
		//Reset our list of gridpositions.
		InitializeList ();
		// Create a random number of inert obstacles, calling Devin's method
		PopulateRocks(3, 5);
		// Create a random number of interactable items, calling Devin's method
		PopulateInteractables(5,10, 100);
		Debug.Log ("TREASURE ROOM!!!");

	}
		
}
