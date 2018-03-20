using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AP_RoomType : MonoBehaviour {

	protected Vector2 center;
	protected int size;
	protected float extents;
	protected enum roomObj {empty, door, item, enemy};
	protected roomObj [,] roomObjects;		// stores a multi dimensional array of roomObj to track what objects have been placed in what positions of the room.

	// track positions of enemies and items placed so far to avoid overlapping object placement.
	protected List<Vector2> enemyPositions;
	protected List<Vector2> itemPositions;

	public virtual void SetBounds(AP_Room room)
	{

		center = room.GetPosition();
		size = room.GetSize();
		extents = size / 2f;
		roomObjects = new roomObj[size, size];
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				roomObjects [i, j] = roomObj.empty;
			}
		}
	}

	protected Vector2 GetPosInRoom(int inset)
	{
		// get a random positin within the room, inset from the bounds of the room
		return Vector2.zero;
	}

	protected Vector2 GetPosAtPerimeter(int dist)
	{
		// get a random position within room, within dist of room edge 
		return Vector2.zero;
	}

}
