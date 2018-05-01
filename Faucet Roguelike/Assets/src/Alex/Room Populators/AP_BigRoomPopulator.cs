using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_BigRoomPopulator : AP_RoomPopulator {

	bool handleBoss = false;
	protected Vector3 offset = Vector2.zero;							// offset of pouplating objects to match big room

	public override void Setup(AP_Room r, DD_GenObstacle g, GiveEnemy e)
	{
		obstacleGenerator = g;
		enemyGenerator = e;
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

	protected virtual void MakeBoss (int min, int max)
	{		// code to be used until Garrett's class/method for providing an enemy object is in place
		int enemyAmount = Random.Range (min, max + 1);
		for (int i = 0; i < enemyAmount; i++)
		{
			GameObject enemy = Instantiate (enemyGenerator.getBoss ());
			Collider2D roomCol = this.gameObject.GetComponent<Collider2D> ();
			enemy.GetComponent<Enemy> ().setRoom (roomCol);				// enemy objects want to have the collider of the room they belong to, so 
			Set (enemy);												// they know the bounds they can wander
		}

	}

	protected override void Set(GameObject thing)
	{
		Vector3 randomPosition = RandomPosition ();
		thing.transform.parent = room.transform;
		thing.transform.localPosition = randomPosition + offset;
	}
}
