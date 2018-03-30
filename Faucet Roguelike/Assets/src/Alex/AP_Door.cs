using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_Door : ScriptableObject
{
	//    Room parentRoom;    // which room does this door come from?
	Vector2 origin;     // unit position of the room this door is expanding from
	Vector2 doorDir;    //direction of this doors expansion. If it opens to the right, doorDir = (0,1)

	public void Setup(AP_Room r, Vector2 dir)
	{
		//parentRoom = r;
		origin = r.GetUnitPos();
		doorDir = dir;
	}

	//    public Room GetParent()
	//    { return parentRoom; }
	public Vector2 GetOrigin()
	{return origin;}
	public Vector2 GetDir()
	{ return doorDir; }
	public Vector2 GetNewPos()
	{ return origin + doorDir; }
}
