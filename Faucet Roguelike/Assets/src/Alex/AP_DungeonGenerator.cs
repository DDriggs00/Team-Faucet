using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AP_DungeonGenerator : MonoBehaviour {
	/*	*Expand – means to generate a connecting room with its own exits, adding those exits to a list for further expansion.
        Step 1: Generate starting room and exits
        Step 2: Expand dungeon through one valid exit, add other exits to a list. 
        Step 3: Continue expanding dungeon through a single new valid exit until randomly chosen distance to middle is reached.
        Step 4: Generate middle room with exits.
        Step 5: Continue expanding dungeon through a single new valid exit until 	randomly chosen distance from middle to end is reached.
        Step 6: Continue expanding dungeon from all other exits added to original list.
        Step 7: choose and delete some percentage of superfluous rooms that lead to 	the same end room.
        Step 8: Generate dungeon room visuals (doors, decorations, etc.)	
        Step 9: populate rooms with interacting items.
    Step 10: populate rooms with enemies.
    Step 11: spawn player in start room. 

        For simplifying generation, I am using Unit Position in this class.
        The room class then handles scaling these unit positions to the size of a room. For example, a 1x1 room is 15x15 units in unity scene.
        As far as this class is concerned, a room that is actually 15x15 centered at position (0, 30) in scene looks like a 1x1 room at (0,2)
        i'm calling this 1x1 a unit size, and this (0,2) the unit position
    */

	int mRoomCount = 0;  // this increments with each new room and is used to assign room IDs
	// cardinal directions used throughout generation
	Vector2 mNorth = new Vector2(0,1), mSouth = new Vector2(0,-1), mEast = new Vector2(1,0), mWest = new Vector2(-1, 0);

	[SerializeField]
	AP_Room mDefaultRoom;
	[SerializeField]
	List<AP_Room> DungeonRooms = new List<AP_Room>();
	List<AP_Door> ExpandingDoors = new List<AP_Door>();
	Vector2 bannedMainPathDir;

	protected struct Count
	{
		public int minimum, maximum;
		public Count(int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
	Count mMainPath = new Count (8, 12);							// min/max main path rooms. eventually to be modified by difficulty/level
	float minMainPathPortion = 0.25f;                                   // min main path number of rooms as a portion of total main path
	Count mBranches = new Count (3, 7);							// min/max number of branches    
	Count mBranchLength = new Count(2,5);							// min/max branch length

	[Header("Populating Objects")]
	public GameObject player;
	public GameObject enemy, dummy;
	public GameObject interactable;
	public GameObject exit;

	public LineRenderer pathDisplay;
	[SerializeField]
	bool displayPath = false;

	public bool populate = false;

	public void Awake()
	{
		Debug.Log ("DG Log - Started Dungeon Generator" +
			"\nIf Unity freezes after this, let Alex know the last DG Log that is printed");
		// ban a direction for main path
		bannedMainPathDir = GetRandomDirections()[0];
		Debug.Log ("DG Log - banned main path direction");
		// GenerateDungeon
		GenerateDungeon();
		Debug.Log ("DG Log - generated dungeon data");
		// generate sprites for each room
		DisplayDungeon();
		Debug.Log ("DG Log - displayed dungeon");

		SpawnPlayer ();
		Debug.Log ("DG Log - Spawned player object");
		PopulateItems ();
		Debug.Log ("DG Log - Populate item for testing");
		PopulateEnemies ();
		Debug.Log ("DG Log - Populate enemy for testing");

		if (populate)
		{
			PopulateDungeon ();
			Debug.Log ("DG Log - Populate dungeon");
		}

		Debug.Log ("DG Log - DG FINISHED");
	}

	void Update()
	{
		if (displayPath)
		{
			DisplayPath (GetPathThroughDungeon ());
			displayPath = false;
		}
	}

	void GenerateDungeon()
	{
		
		int dungeonSpan = Random.Range(mMainPath.minimum, mMainPath.maximum+1);
		int minDist = Mathf.CeilToInt(minMainPathPortion * dungeonSpan); 
		int countToMid= Random.Range(minDist, dungeonSpan - minDist);
		int countToEnd = dungeonSpan - countToMid;

		AP_Room startRoom = CreateRoom(Vector2.zero);               //create starting room, add it to dungeon rooms list
		startRoom.SetRoomType(AP_Room.RoomType.start);
		ExpandMainPathDoors(startRoom);								// add doors to start room, then call expand making sure rooms only expand 'away' from start room

		for (int i = 0; i < countToMid; i++)						// builds path between start room and mid room
		{                                                           
			AP_Room newRoom = ExpandRoom();
			newRoom.SetRoomType(AP_Room.RoomType.mainPath);
			if (i != countToMid - 1)								// I only main path to continue expanding until the next room is the middle room
				ExpandMainPathDoors(newRoom);	
		}
			
		BuildBigRoom(false);                                        // passing false tells method this is the middle room

		for (int i = 0; i < countToEnd; i++)						// expand main path from mid room to end room
		{                                                           
			AP_Room newRoom = ExpandRoom();
			newRoom.SetRoomType(AP_Room.RoomType.mainPath);
			if (i != countToEnd - 1)								// no more main path when the  next room is the end room
				ExpandMainPathDoors(newRoom);
		}
			
		BuildBigRoom(true);											// passing true tells method this is the end room

		GenerateBranchPaths();

		// go through dungeon rooms to % change merge some together.
		// go through mainPath and branch room types for change to convert them to trap rooms
	}

	void BuildBigRoom(bool isEndRoom)
	{
		Vector2 origin = DungeonRooms[DungeonRooms.Count - 1].GetUnitPos();	// get unit position of last room in dungeon room list to know the previous room to big room
		Vector2[] dirs = GetRandomDirections();
		Vector2[] sectionOffsets = {Vector2.zero, Vector2.right, new Vector2(1,1), Vector2.up};	// offsets of four rooms composing the big room with 0,0 as the bottom left room section
		Vector2[] sectionPositions = new Vector2[4];
		bool isValid = true;
		int curDirIndex = 0;
		int offset = 0;                             // stores random offset of big room from origin room
		offset = (Random.value > 0.5) ? 0 : -1;     // for now as big rooms are 2x2, either no offset, or -1 offset are possible
		int attempts = 0;                           // used to track attempt iterations to place big room, to use both possible offsets 
		do
		{
			isValid = true;
			Vector2 curDir = dirs[curDirIndex];

			if (dirs[curDirIndex] == mNorth)
			{ sectionPositions[0] = origin + curDir + new Vector2(offset, 0);}
			else if (dirs[curDirIndex] == mEast)
			{ sectionPositions[0] = origin + curDir + new Vector2(0, offset);}
			else if (dirs[curDirIndex] == mSouth)
			{ sectionPositions[0] = origin + curDir + new Vector2(offset, -1);}
			else // dir[curDirIndex] must equal mWest
			{ sectionPositions[0] = origin + curDir + new Vector2(-1, offset);}
			for(int i = 1; i < 4; i++)                                          // using 0,0 as bottom left, setup other section positions
			{ sectionPositions[i] = sectionPositions[0] + sectionOffsets[i];}
			// iterate through vector2 array. if all positions are free space, continue
			for(int r = 0; r < 4; r++)
			{
				if (!isSpaceAvailable(sectionPositions[r]))
				{
					isValid = false;
					curDirIndex++;
					if (attempts == 0)                                          // swap the offset and try again
					{
						curDirIndex = 0;
						attempts = 1;
						offset = (offset == 0) ? -1 : 0;
					}
				}
			}
		} while (!isValid && curDirIndex < 4);								// repeat trying different positioning of big room until valid position is found

		if (curDirIndex > 3)                                                     //reload level if big room fails to generate in current conditions
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

			//            Application.LoadLevel(Application.loadedLevelName);
			print("RELOAD LEVEL " + ((isEndRoom) ? "END ROOM" : "MID ROOM") + " FAILED");
			do {
			} while(Application.isLoadingLevel);

			return; // return from this function so no other lines in this function run, this should theoretically be unnecessary?
		}

		for(int i = 0; i < 4; i++)                                          // iterate through array again, generating a room at each chosen unit position
		{                                   
			AP_Room r = CreateRoom(sectionPositions[i]);
			r.SetRoomType(((isEndRoom) ? AP_Room.RoomType.end : AP_Room.RoomType.mid));
			// consider expanding rooms of big room using add door to end, to enable potential branching from big room

		}
		// merge the four created rooms
		GetRoomAtPos(sectionPositions[0]).MergeRoom(GetRoomAtPos(sectionPositions[1]));
		GetRoomAtPos(sectionPositions[0]).MergeRoom(GetRoomAtPos(sectionPositions[3]));
		GetRoomAtPos(sectionPositions[2]).MergeRoom(GetRoomAtPos(sectionPositions[1]));
		GetRoomAtPos(sectionPositions[2]).MergeRoom(GetRoomAtPos(sectionPositions[3]));

		AP_Room entryRoom = GetRoomAtPos(origin);						
		AP_Room midRoomNode = GetRoomAtPos(origin + dirs[curDirIndex]);
		entryRoom.ConnectRoom(midRoomNode);									// connect this big room with the last main path room generated

		if (!isEndRoom)
		{
			// choose a valid direction and room for the main path to continue on from
			int entryDoor = 0;
			int exitDoor = 0;
			for (int i = 0; i < 4; i++)
			{
				if (Vector2.Distance(origin, sectionPositions[i]) == 1)
					entryDoor = i;
			}
			exitDoor = (entryDoor + 2 > 3) ? (entryDoor + 1) % 3 : entryDoor + 2;
			//            print("entry door = " + entryDoor + "   exit door = " + exitDoor);
			for(int i = 0; i < 4; i++)
			{
				if(i != exitDoor)
					ExpandMainPathDoors(GetRoomAtPos(sectionPositions[i]));
			}
			ExpandMainPathDoors(GetRoomAtPos(sectionPositions[exitDoor]));
		}
	}

	void ExpandMainPathDoors(AP_Room r)
	{
		Vector2[] dirs = GetRandomDirections();                                     // get shuffled array of cardinal directions
		for (int i = 0; i < 4; i++)
		{                                                                           // iterate through array to find first valid direction

			if (isSpaceAvailable(r, dirs[i]))       								// direction doens't already have a room
			{
				if(dirs[i] != bannedMainPathDir)									// direction isn't banned for main path
				{
					AddDoorToFront(r, dirs[i]);
					for(int j = i; j < 4; j++)
					{
						AddDoorToEnd (r, dirs [j]);
					}
					break;
				}
				else
					AddDoorToEnd(r, dirs[i]);        								// add door at end as potential branch if banned from main path                              
			}
		}
	}

	void GenerateBranchPaths()
	{
		ShuffleDoors();
		List<AP_Room> 	branchEndRooms = new List<AP_Room> ();
		List<int> 		branchLengths = new List<int> ();
		int branchCount = Random.Range(mBranches.minimum, mBranches.maximum + 1);						        // get random number of branches to be created
		//		print ("Branch Count = " + branchCount);
		for(int i = 0; i < branchCount; i++)														
		{
			if (ExpandingDoors.Count == 0)															// can't branch if there are no available doors
				break;
			Vector2 pos = ExpandingDoors[0].GetOrigin() + ExpandingDoors[0].GetDir();				// get proposed new room position
			if(isSpaceAvailable(pos))																
			{
				int length;
				AP_Room lastRoom;
				GenerateBranch(out length, out lastRoom);											// generate branch starting from that door
				if (length > 0)
				{
					branchEndRooms.Add (lastRoom);
					branchLengths.Add (length);
				}
			} else
			{
				ExpandingDoors.RemoveAt(0);															// remove bad door and decrement i so branch count is preserved
				i--;
			}
		}
		// for each room in branchEndRooms
		for(int i = 0; i < branchEndRooms.Count; i++)
		{
			AP_Room r = branchEndRooms [i];
			// check to see if end room can connect to another room and isn't already connected to more than one other room
			// connect it to another room if possible
			Vector2[] dirs = GetRandomDirections();
			int connectionsMade = 0;
			for(int j = 0; j < 4; j++)
			{
				if (!r.IsRoomConnectedInDir(dirs[j])
					&& !isSpaceAvailable (r.GetUnitPos () + dirs [j])
					&& GetRoomAtPos (r.GetUnitPos () + dirs [j]).GetRoomType () != AP_Room.RoomType.end)
				{
					//					print ("Connect rooms");
					r.ConnectRoom (GetRoomAtPos (r.GetUnitPos () + dirs[j]));
					connectionsMade++;
				}

			}
			if (connectionsMade == 0 && branchLengths[i] > (mBranchLength.minimum + mBranchLength.maximum) /2) // if no connection was made on last room and length is greater than a certain value
				// % chance to change room into treasure room
			{
				//				print ("TREASURE ROOM MADE AT INDEX" + DungeonRooms.IndexOf(r));
				r.SetRoomType(AP_Room.RoomType.treasure);
			}

		}

	}

	void GenerateBranch(out int length, out AP_Room lastRoom)
	{		// out above used to return length and lastRoom of branch to GenerateBranchPaths() for making connections at the end
		int branchLength = Random.Range(mBranchLength.minimum, mBranchLength.maximum + 1);
		//		print ("Branch Length = " + branchLength);
		length = 0;
		lastRoom = null;
		for (int i = 0; i < branchLength; i++)
		{
			length++;
			AP_Room newBranchRoom = ExpandRoom();
			newBranchRoom.SetRoomType(AP_Room.RoomType.branch);

			if (i < branchLength - 1)
			{
				if (!ExpandBranchDoors (newBranchRoom))
				{
					lastRoom = newBranchRoom;
					break;
				}
			} else
				lastRoom = newBranchRoom;
		}
	}

	void ShuffleDoors()
	{
		for (int d = 0; d < ExpandingDoors.Count; d++)
		{  
			AP_Door tempDoor = ExpandingDoors[d];
			int r = Random.Range(d, ExpandingDoors.Count);
			ExpandingDoors[d] = ExpandingDoors[r];
			ExpandingDoors[r] = tempDoor;
		}
	}


	bool ExpandBranchDoors(AP_Room r)
	{
		Vector2[] dirs = GetRandomDirections();                                     // get shuffled array of cardinal directions
		for (int i = 0; i < 4; i++)
		{                                                                           // iterate through array to find first valid direction
			if (isSpaceAvailable(r, dirs[i]))
			{
				AddDoorToFront(r, dirs[i]);
				return true;
			}
		}
		return false;
	}

	AP_Room ExpandRoom()
	{
		if (ExpandingDoors.Count > 0)
		{
			AP_Door nextDoor = ExpandingDoors[0];                                          // get most recent door from expanding doors list
			ExpandingDoors.RemoveAt(0);                                                 // removing door from list makes sure it's not used again
			AP_Room newRoom = CreateRoom(nextDoor.GetNewPos());                            // place room based on door retrieved                                                       
			AP_Room originRoom = GetRoomAtPos(nextDoor.GetOrigin());                      // get origin of nextDoor, then find that origin room in dungeon rooms
			originRoom.ConnectRoom(newRoom);                                           // join both rooms with a door edge
			return newRoom;
		}
		else
		{
			print("NO DOORS TO EXPAND FROM");
			return null;
		}

	}

	AP_Room CreateRoom(Vector2 rPos)
	{
		if (isSpaceAvailable (rPos)) {

			AP_Room newRoom = Instantiate (mDefaultRoom) as AP_Room;
			newRoom.SetupRoom (rPos, mRoomCount++);                                  // room count used to set the room id, and then increment room count so next set id will be different
			DungeonRooms.Add (newRoom);
			return newRoom;
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			//			Application.LoadLevel (Application.loadedLevelName);
			print("RELOAD LEVEL - FAILED TO CREATE ROOM");
			do {
			} while(Application.isLoadingLevel);
			return null;
		}
	}

	void AddDoorToFront(AP_Room r, Vector2 dir)
	{
		AP_Door newDoor = CreateDoor(r, dir);
		ExpandingDoors.Insert(0, newDoor);
	}

	void AddDoorToEnd(AP_Room r, Vector2 dir)
	{
		AP_Door newDoor = CreateDoor(r, dir);
		ExpandingDoors.Insert(ExpandingDoors.Count, newDoor);
	}

	AP_Door CreateDoor(AP_Room r, Vector2 dir)
	{
		AP_Door newDoor = ScriptableObject.CreateInstance<AP_Door>();
		newDoor.Setup(r, dir);
		return newDoor;
	}

	bool isSpaceAvailable(AP_Room r, Vector2 dir)
	{
		return isSpaceAvailable(r.GetUnitPos() + dir);
	}

	bool isSpaceAvailable(Vector2 pos)
	{
		foreach (AP_Room r in DungeonRooms)
		{
			if (r.GetUnitPos() == pos)                 // returns true if room containst he Unit Position pos
				return false;
		}
		return true;
	}

	void DisplayDungeon()
	{
		foreach(AP_Room r in DungeonRooms)
		{
			r.GenerateRoom();
		}
	}

	AP_Room GetRoomAtPos(Vector2 pos)
	{
		foreach (AP_Room r in DungeonRooms)
		{
			if (r.GetUnitPos() == pos)                 // returns true if room containst he Unit Position pos
				return r;
		}
		return null;
	}

	Vector2[] GetRandomDirections()         // returns array of 4 cardinal directions in a random order
	{
		Vector2[] dirs = { mNorth, mEast, mWest, mSouth };
		for (int i = 0; i < dirs.Length; i++)
		{
			Vector2 temp = dirs[i];
			int r = Random.Range(i, dirs.Length);
			dirs[i] = dirs[r];
			dirs[r] = temp;
		}
		return dirs;
	}

	public AP_Room GetStartingRoom()
	{ return DungeonRooms[0]; }

	void SpawnPlayer()
	{
		GameObject p = Instantiate (player);
		p.transform.position = GetStartingRoom ().GetPosition ();
	}

	void PopulateEnemies()
	{
		GameObject newEnemy = Instantiate (enemy);
		newEnemy.transform.position = DungeonRooms [2].GetPosition ();
		Collider2D roomCol = DungeonRooms [2].GetComponent<Collider2D> ();
		newEnemy.GetComponent<Enemy> ().setRoom (roomCol);
		// create dummy
		GameObject d = Instantiate (dummy);
		d.transform.position = DungeonRooms [3].GetPosition ();
	}

	void PopulateItems()
	{
		GameObject item = Instantiate (interactable);
		item.transform.position = DungeonRooms [1].GetPosition ();

		Vector2 endRoomCenter = Vector2.zero;
		foreach (AP_Room r in DungeonRooms)
		{
			if (r.roomType == AP_Room.RoomType.end)
				endRoomCenter += r.GetPosition ();
		}
		endRoomCenter /= 4;
		GameObject ex = Instantiate (exit);
		ex.transform.position = endRoomCenter;
	}

	void PopulateDungeon()
	{
		foreach (AP_Room r in DungeonRooms)
		{
			AP_RoomPopulator pop = r.GetPop ();
			pop.PopulateRoom (1);
		}
	}

	public List<Vector2> GetPathThroughDungeon()
	{
		List<Vector2> path = new List<Vector2> ();
		AP_Room curRoom = DungeonRooms [0];


		int curRoomIndex = 0;
		do 
		{
			// get curRoom based on curRoomIndex
			curRoom = DungeonRooms [curRoomIndex];
			// if curRoom is main path
			//			print("current room type = " + curRoom.GetRoomType());
			if(curRoomIndex == 0 || curRoom.GetRoomType() == AP_Room.RoomType.mainPath)
			{
				//				print("GOT ROOM AT INDEX " + curRoomIndex);
				// get curRoom pos and add it to path list
				Vector2 roomPos = curRoom.GetPosition();
				path.Add(roomPos);
				// increment curRoomIndex
				curRoomIndex++;
			}
			// else if curRoom is mid room
			else if (curRoom.GetRoomType() == AP_Room.RoomType.mid)
			{

				Vector2 closeEntry = curRoom.GetPosition();
				Vector2 farExit = curRoom.GetPosition();
				for(int i = 1; i < 4; i++)	// increment through the 4 rooms of the mid room to get the room closest and furthest from last room. These will be the entry/exit rooms
				{
					Vector2 previousRoomPos = DungeonRooms[curRoomIndex -1].GetPosition();

					float distance = Vector2.Distance(previousRoomPos, DungeonRooms[curRoomIndex + i].GetPosition());
					if(distance < Vector2.Distance(previousRoomPos, closeEntry))
						closeEntry = DungeonRooms[curRoomIndex + i].GetPosition();
					else if (distance > Vector2.Distance(previousRoomPos, farExit))
						farExit = DungeonRooms[curRoomIndex + i].GetPosition();

				}
				// add entry room of mid room pos, add to list
				path.Add(closeEntry);
				// add exit room of mid room pos, add to list
				path.Add(farExit);
				// curRoomIndex += 4, to get the index to the next room outside the mid room
				curRoomIndex+= 4;
			}

			// else if curRoom is end room
			else if (curRoom.GetRoomType() == AP_Room.RoomType.end)
			{
				Vector2 closeEntry = curRoom.GetPosition();
				Vector2 centroid = closeEntry;
				for(int i = 1; i < 4; i++)	// increment through the 4 rooms of the mid room to get the room closest and furthest from last room. These will be the entry/exit rooms
				{
					centroid += DungeonRooms[curRoomIndex + i].GetPosition();

					Vector2 previousRoomPos = DungeonRooms[curRoomIndex -1].GetPosition();
					float distance = Vector2.Distance(previousRoomPos, DungeonRooms[curRoomIndex + i].GetPosition());

					if(distance < Vector2.Distance(previousRoomPos, closeEntry))
						closeEntry = DungeonRooms[curRoomIndex + i].GetPosition();
				}
				centroid /= 4;
				// add entry room of mid room pos, add to list
				path.Add(closeEntry);
				// add exit room of mid room pos, add to list
				path.Add(centroid);
				// add entry room of end room pos, add to list
				// add centeroid of all rooms in end room to list
			}

		} while (curRoom.GetRoomType () != AP_Room.RoomType.end);

		return path;
	}

	void DisplayPath(List<Vector2> path)
	{
		for (int i = 0; i < path.Count - 1; i++)
		{
			LineRenderer newLine = Instantiate (pathDisplay);
			newLine.SetPosition (0, (Vector3) path [i] + new Vector3(0,0,-1));
			newLine.SetPosition (1, (Vector3) path [i + 1] + new Vector3(0,0,-1));
		}
	}

}
