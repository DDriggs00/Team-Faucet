using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_Room : MonoBehaviour
{
	public GameObject cDoorEdge, cOpenEdge, cWallEdge, cFloor;
	public GameObject cWallCorner, cStraightWall, cOpenFloor;

	public GameObject[] cDoorEdges, cWallEdges, cOpenEdges,	// 0 is top, 1 left, 2 bot, 3 right, think counter-clockwise
						cInnerCorners,	// 0 top left, work counterclockwise
						cWallSegments;	// 0 top, work counterclockwise
	[SerializeField]
	float mUnitSize = 15;

	Vector2 mUnitPos;                                               // unit position of this given room
	enum EdgeType { open, door, wall };                             // used to describe edge's type

	EdgeType[] mRoomEdges = {   EdgeType.wall, EdgeType.wall,       //mRoomEdges used to keep track of what each edge of the room should build as
		EdgeType.wall, EdgeType.wall };     // 0 = north, 1 = west, 2 = south, 3 = east
	int roomID;                                                     // merged rooms will share roomIDs, used by generator to find all pieces of larger rooms

	public enum RoomType { start, mid, end, mainPath, branch, treasure, trap, baddy};
	public RoomType roomType;
	List<AP_Room> mMergedRooms = new List<AP_Room>();
	AP_RoomPopulator roomPop;

	public GameObject[] traps;
	public GameObject [] treasure;
	public GameObject [] enemies;
	public GameObject [] obstacles;

	public void SetupRoom(Vector2 pos, int id)
	{
		SetUnitPos(pos);
		this.transform.position = pos * mUnitSize;
		SetID(id);
		roomPop = GetRoomPopulator();		// Add room populator based on room type
		roomPop.Setup (this);
	}

	public void GenerateRoom()
	{
		for(int i = 0; i < 4; i++)
		{
			GameObject newEdge = Instantiate(GetEdge(mRoomEdges[i], i));
			newEdge.transform.position = mUnitPos * mUnitSize;
//			newEdge.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 90));
			newEdge.transform.parent = this.transform;
		}
		//Generate Corners
		for (int c = 0; c < 4; c++) 
		{
			GameObject cornerObj = cWallCorner;
			Vector2 cornerPos = Vector2.zero;
			float rotation = 90;
			switch (c) 
			{
			case 0:		//top left
				cornerObj = GetCorner(c, 0, 1);
				cornerPos = new Vector2 (-mUnitSize / 2 + .5f, mUnitSize / 2 - .5f);
				break;
			case 1: 	// bot left
				cornerObj = GetCorner(c, 2, 1);
				cornerPos = new Vector2 (-mUnitSize / 2 + .5f, -mUnitSize / 2 + .5f);
				break;
			case 2: 	// bot right
				cornerObj = GetCorner(c, 2, 3);
				cornerPos = new Vector2 (mUnitSize / 2 - .5f, -mUnitSize / 2 + .5f);
				break;
			default:	// top right
				cornerObj = GetCorner(c, 0, 3);
				cornerPos = new Vector2 (mUnitSize / 2 - .5f, mUnitSize / 2 - .5f);
				break;
			}
			GameObject newCorner = Instantiate (cornerObj);
			newCorner.transform.parent = this.transform;
			newCorner.transform.localPosition = cornerPos;
		}


		GameObject floor = Instantiate(cFloor);
		floor.transform.position = mUnitPos * mUnitSize;
		floor.transform.parent = this.transform;
		this.transform.position += new Vector3 (0, 0, 1);	// drop room down so objects, enemy, and player will be closer to camera
	}

	public void SetUnitPos(Vector2 p)
	{ mUnitPos = p; }
	public Vector2 GetUnitPos()
	{ return mUnitPos; }
	public Vector2 GetPosition()
	{ return transform.position; }
	public int GetSize()
	{ return (int)mUnitSize;}


	public void SetDoor(Vector2 d)
	{ mRoomEdges[GetEdgeIndex(d)] = EdgeType.door; }
	public void SetWall(Vector2 d)
	{ mRoomEdges[GetEdgeIndex(d)] = EdgeType.wall; }
	public void SetOpen(Vector2 d)
	{ mRoomEdges[GetEdgeIndex(d)] = EdgeType.open; }


	public void MergeRoom(AP_Room r)
	{
		Vector2 mergeDir = r.GetUnitPos() - GetUnitPos();
		if(mergeDir.magnitude != 1)
		{
			print("Rooms not orthogonally adjacent, merge failed");
			return;
		}
		MergeRoom(r, GetID());
		r.MergeRoom(this, GetID());
	}
	public void MergeRoom(AP_Room r, int id)
	{
		Vector2 mergeDir = r.GetUnitPos() - GetUnitPos();
		SetOpen(mergeDir);
		SetID(id);
		mMergedRooms.Add(r);

	}

	public void ConnectRoom(AP_Room r)
	{
		Vector2 connectDir = r.GetUnitPos() - GetUnitPos();
		if (connectDir.magnitude != 1)
		{
			print("Rooms not orthogonally adjacent, connect failed");
			return;
		}
		ConnectRoom(connectDir);
		r.ConnectRoom(-connectDir);
	}
	public void ConnectRoom(Vector2 dir)
	{
		SetDoor(dir);
	}
	public bool IsRoomConnectedInDir(Vector2 dir)
	{
		if (mRoomEdges [GetEdgeIndex (dir)] == EdgeType.door)
			return true;
		else
			return false;
	}


	public bool IsMerged()
	{
		for(int i = 0; i < 4; i++)
		{
			if (mRoomEdges[i] == EdgeType.open)
				return true;
		}
		return false;
	}

	public void SetID(int id)
	{ roomID = id; }
	public int GetID()
	{ return roomID; }

	int GetEdgeIndex(Vector2 v)
	{   // given v, return the associated mRoomEdges index 
		if (v == Vector2.up)            // north
			return 0;
		else if (v == Vector2.left)     // west
			return 1;
		else if (v == Vector2.down)     // south
			return 2;
		else                            // east                
			return 3;
	}

	GameObject GetEdge(EdgeType et, int side)
	{
		switch (et)
		{
		case EdgeType.door:
			return cDoorEdges[side];
		case EdgeType.open:
			return cOpenEdges[side];
		case EdgeType.wall:
			return cWallEdges[side];
		}
		return cWallEdges[side];
	}

	GameObject GetCorner(int corner, int vertSide, int horSide)
	{
		// if sideOne and SideTwo are both open, return floor tile
		if (mRoomEdges [vertSide] == EdgeType.open && mRoomEdges [horSide] == EdgeType.open)
		{
			return cOpenFloor;
		}
		// if sideOne and Side Two are neither open, return wall corner
		else if (mRoomEdges [vertSide] != EdgeType.open && mRoomEdges [horSide] != EdgeType.open)
		{
			return cInnerCorners [corner];
		}
		// if only one side is open, return a wall edge
		else
		{
			if(mRoomEdges[vertSide] == EdgeType.open)
			{
				switch(corner)
				{
				case 0:			// top left, return left wall
					// break left out intentionally
				case 1:			// bot left, return left wall
					return cWallSegments[1];
					break;
				case 2:			// bot right, return right wall
	//				break left out intentionally
				default:		// top right, return right wall
					return cWallSegments[3];
					break;
				}
			}
			else
			{
				switch(corner)
				{
				case 0:			// top left, return top wall
					return cWallSegments[0];
					break;
				case 1:			// bot left, return bot wall
					// break left out intentionally
				case 2:			// bot right, return bot wall
					return cWallSegments[2];
					break;
				default:		// top right, return top wall
					return cWallSegments [0];
					break;
				}
			}
		}

	}

	public void SetRoomType(RoomType rType)
	{ roomType = rType;}
	public RoomType GetRoomType()
	{ return roomType; }
	public AP_RoomPopulator GetPop()
	{ return roomPop; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			AP_CameraController cam = collision.GetComponent<AP_CameraController> ();
			if(mMergedRooms.Count == 0)
				cam.SetTarget(transform.position);
			else if (!cam.IsInBigRoom())
			{
				print("merged room count = " + mMergedRooms.Count);
				float minX = transform.position.x;
				float maxX = minX;
				float minY = transform.position.y;
				float maxY = minY;

				foreach(AP_Room r in mMergedRooms)
				{
					Vector2 v = r.GetPosition();
					if (v.x < minX)
						minX = v.x;
					else if (v.x > maxX)
						maxX = v.x;
					if (v.y < minY)
						minY = v.y;
					else if (v.y > maxY)
						maxY = v.y;
				}
				cam.SetInBigRoom(minX, maxX, minY, maxY);

			}
		}
	}

	AP_RoomPopulator GetRoomPopulator()
	{
		AP_RoomPopulator pop;

		switch (roomType)
		{
		case RoomType.start:
			pop = gameObject.AddComponent<AP_StartRoomPopulator> ();
			break;
		case RoomType.mid:
			pop = gameObject.AddComponent<AP_MidRoomPopulator> ();
			break;
		case RoomType.end:
			pop = gameObject.AddComponent<AP_EndRoomPopulator> ();
			break;
		case RoomType.mainPath:
			pop = gameObject.AddComponent<AP_PathRoomPopulator> ();
			break;
		case RoomType.treasure:
			pop = gameObject.AddComponent<AP_TreasureRoomPopulator> ();
			break;
		case RoomType.trap:
			pop = gameObject.AddComponent<AP_TrapRoomPopulator> ();
			break;
		case RoomType.baddy:
			pop = gameObject.AddComponent<AP_BaddyRoomPopulator> ();
			break;
		default:
			pop = gameObject.AddComponent<AP_BranchRoomPopulator> ();
			break;
		}
		 
		return pop;
	}

}
