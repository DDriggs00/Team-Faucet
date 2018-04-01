using System.Collections;
using System.Collections.Generic; 		//Allows us to use Lists.
using UnityEngine;
using System;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public abstract class AP_RoomPopulator : MonoBehaviour {


	protected int rows, columns; 										//Number of rows and columns in our game board.
	protected Vector3 offset = Vector2.zero;							// offset of pouplating objects, equal to center of associated room

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
		
	public virtual void PopulateRoom (int level)
	{
		
	}

	protected virtual void PopulateRocks(int min, int max)
	{
		int obstacleAmount = Random.Range(min, max+1);
		for (int o = 0; o < obstacleAmount; o++)
		{
			GameObject rock = Instantiate (obstacleGenerator.generateRock ());
			Set (rock);
		}
	}

	protected virtual void PopulateInteractables(int min, int max, int chanceGood)
	{
		int interactableAmount = Random.Range(min, max+1);
		for (int i = 0; i < interactableAmount; i++)
		{
			GameObject interactable = Instantiate (obstacleGenerator.generateInteractable(chanceGood));
			Set (interactable);
		}
	}

	protected virtual void PopulateEnemies(int min, int max)
	{
		//Determine number of enemies based on current level number, based on a logarithmic progression
		//		int enemyCount = (int)Mathf.Log(level, 2f);
		//		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
//TEMP CODE
		GameObject e = FindObjectOfType<AP_DungeonGenerator>().enemy;
		int enemyAmount = Random.Range (min, max + 1);
		for (int i = 0; i < enemyAmount; i++)
		{
			GameObject enemy = Instantiate (e);
			Collider2D roomCol = this.gameObject.GetComponent<Collider2D> ();
			enemy.GetComponent<Enemy> ().setRoom (roomCol);
			Set (enemy);
		}

	}

	protected void Set(GameObject thing)
	{
		Vector3 randomPosition = RandomPosition ();
		thing.transform.parent = room.transform;
		thing.transform.localPosition = randomPosition;
	}




}
