using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_StartRoomPopulator : AP_RoomPopulator {

	public override void Setup(AP_Room r, DD_GenObstacle g)
	{
		obstacleGenerator = g;
		room = r;
		rows = r.GetSize () - 3;
		columns = rows;
		offset = r.GetPosition ();

		obstacleCount = new Count (0, 0);
		interactableCount = new Count (0, 0);
	}
}
