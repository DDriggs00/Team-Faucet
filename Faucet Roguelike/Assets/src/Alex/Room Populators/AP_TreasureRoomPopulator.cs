using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_TreasureRoomPopulator : AP_BranchRoomPopulator {


	public override void Setup(AP_Room r, DD_GenObstacle g)
	{
		obstacleGenerator = g;
		room = r;
		rows = r.GetSize () - 3;
		columns = rows;
		offset = r.GetPosition ();

		obstacleCount = new Count (3, 5);
		interactableCount = new Count (5, 12);
	}

	public override void PopulateRoom (int level)
	{
		print ("TREASURE ROOM!!!");
		//Reset our list of gridpositions.
		InitializeList ();

		//Instantiate a random number of obstacles based on minimum and maximum, at randomized positions.
		//		LayoutObjectAtRandom (room.obstacles, obstacleCount.minimum, obstacleCount.maximum);

		// Create a random number of inert obstacles, calling Devin's method
		int obstacleAmount = Random.Range(obstacleCount.minimum, obstacleCount.maximum+1);
		for (int o = 0; o < obstacleAmount; o++)
		{
			Vector3 randomPosition = RandomPosition ();
			GameObject rock = Instantiate (obstacleGenerator.generateRock ());
			rock.transform.parent = room.transform;
			rock.transform.localPosition = randomPosition;

		}

		// Create a random number of interactable items, calling Devin's method
		int interactableAmount = Random.Range(interactableCount.minimum, interactableCount.maximum+1);
		for (int i = 0; i < interactableAmount; i++)
		{
			Vector3 randomPosition = RandomPosition ();
			GameObject interactable = Instantiate (obstacleGenerator.generateInteractable(100));
			interactable.transform.parent = room.transform;
			interactable.transform.localPosition = randomPosition;

		}

	}
}
