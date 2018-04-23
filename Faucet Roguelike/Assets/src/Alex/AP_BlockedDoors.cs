using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_BlockedDoors : MonoBehaviour {

	List<GameObject> doorBlocks = new List<GameObject> ();
	public GameObject block;
	Vector2[] dirs = { Vector2.up, Vector2.left, Vector2.down, Vector2.right }; 
	AP_Room room;

	void Awake()
	{
		room = GetComponent<AP_Room> ();
	}

	/*void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			SetupDoors ();
		else if (Input.GetKeyUp (KeyCode.Space))
			BreakBlocks ();
	}*/
	public void SetupDoors()
	{
		doorBlocks.Clear ();
		for (int i = 0; i < 4; i++)
		{
			if (room.IsRoomConnectedInDir (dirs [i]))
				BlockDoor (dirs [i], room.GetSize());
		}
	}

	public void BlockDoor (Vector2 dir, int size)
	{
		Vector2 pos = room.GetPosition ();
		GameObject newBlock = Instantiate (block);
		doorBlocks.Add (newBlock);
		int offset = size / 2;
		newBlock.transform.position = pos + dir * offset;
		Vector2 perp = new Vector2 (dir.y, -dir.x);

		GameObject block2 = Instantiate (block);
		doorBlocks.Add (block2);
		GameObject block3 = Instantiate (block);
		doorBlocks.Add (block3);
		block2.transform.position = pos + dir * offset + perp;
		block3.transform.position = pos + dir * offset - perp;
	}

	public void BreakBlocks()
	{
		FindObjectOfType<ZG_AudioManager> ().playDynamicSound ("boulderBreak");
		foreach (GameObject b in doorBlocks)
		{
			b.GetComponent<Animator> ().enabled = true;
			Destroy (b, 0.56f);
		}

	}
}
