using System.Collections;
using System.Collections.Generic; 		//Allows us to use Lists.
using UnityEngine;
using System;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public abstract class AP_RoomPopulator : MonoBehaviour {


	public int columns = 12; 										//Number of columns in our game board.
	public int rows = 12;											//Number of rows in our game board.
	public Vector3 offset = Vector2.zero;							// offset of pouplating objects, equal to center of associated room

	public struct Count
	{
		public int minimum, maximum;
		public Count(int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
	public Count obstacleCount;					// lower and upper limit for random number of obstacles in room
	public Count interactableCount;

	private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.

	protected AP_Room room;
	protected DD_GenObstacle obstacleGenerator;

	public virtual void Setup(AP_Room r, DD_GenObstacle g)
	{
		obstacleGenerator = g;
		room = r;
		rows = r.GetSize () - 3;
		columns = rows;
		offset = r.GetPosition ();

		obstacleCount = new Count (5, 9);
		interactableCount = new Count (1, 4);
	}

	//Clears our list gridPositions and prepares it to generate a new board.
	protected void InitializeList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();

		//Loop through x axis (columns).
		for(int x = 1; x < columns-1; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 1; y < rows-1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}


	//RandomPosition returns a random position from our list gridPositions.
	protected Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);

		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];

		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
		randomPosition -= new Vector3 (rows / 2f, columns / 2f, 1);
		//Return the randomly selected Vector3 position.
		return randomPosition;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum+1);

		//Instantiate objects until the randomly chosen limit objectCount is reached
		for(int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();

			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];

			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation


			GameObject newObj = Instantiate(tileChoice);//, randomPosition + offset, Quaternion.identity);
			newObj.transform.parent = room.transform;
			newObj.transform.localPosition = randomPosition;
		}
	}


	public virtual void PopulateRoom (int level)
	{
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
			GameObject interactable = Instantiate (obstacleGenerator.generateInteractable(50));
				interactable.transform.parent = room.transform;
				interactable.transform.localPosition = randomPosition;
				
		}

		// Create a semi-random number of enemies, calling Garrett's method.
		//Determine number of enemies based on current level number, based on a logarithmic progression
		//		int enemyCount = (int)Mathf.Log(level, 2f);
		//		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);

	}




}
