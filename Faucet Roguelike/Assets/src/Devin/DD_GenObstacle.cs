// To generate an Obstacle, so the following:
// 1. Create an instance of this class
// 2. Use the following line of code
// GameObject objectName = Instantiate(obstacleGenerator.someFunction());

using System.Collections; 			// Standard Unity include
using System.Collections.Generic;	// Standard Unity include
using UnityEngine;					// Standard Unity include

public class DD_GenObstacle : MonoBehaviour
{
	// The following GameObjects (and lists) are populated with Prefabs in Unity
	public List<GameObject> goodInteractables; // Interactables that help the player
	public List<GameObject> badInteractables;  // Interactables that hinder the player
	public List<GameObject> rocks;	// Inert obstacles
	public GameObject lever;
	public GameObject exit;
	public GameObject dodoEgg;	// The Golden Dodo Egg of Yendor

	// Generate an interactable with a certain chance of being good or bad
	// Weight is an integer between 0 and 100, where 100 = always good and 0 = always bad
	public GameObject generateInteractable(int weight)
	{
		int RandNum = Random.Range(0, 101);	// Will be seeded once the seed system is up
		if(RandNum < weight) 
		{
			// return random good item
			return goodInteractables[Random.Range(0, goodInteractables.Count)];
		}
		else 
		{
			//return random bad item
			return badInteractables[Random.Range(0, badInteractables.Count)];
		}
	}

	// Generates an obstacle that blocks the player and certain enemies
	public GameObject generateRock() 
	{
		return rocks[Random.Range(0,rocks.Count)];
	}

	// Generates an exit (or a Dodo egg on hte final level)
	public GameObject generateExit() 
	{
		DD_Obstacle obstacle = new DD_Obstacle();

		if(obstacle.getLevelNumber() >= obstacle.getMaxLevel()) 
		{
			// If this is the final level
			return dodoEgg;
		}
		else 
		{
			return exit;
		}
	}

	// Generate a lever to lock and unlock the doors to the room
	public GameObject generateLever() 
	{
		return lever;
	}
}