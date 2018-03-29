using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Lever : DD_Obstacle
{

	private bool mCanActivate = false;
	private bool mLeverState = false; //true = on, false = off
	DD_Lever()
	{
		Debug.Log("Lever Created");
		SetObstacleType(ObstacleType.lever);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if(collision.tag == "Player")
		{
			LH_Interact playerInteract = collision.gameObject.GetComponent<LH_Interact>();
			playerInteract.AssignInteractible(this);
			mCanActivate = true;
			Debug.Log("Player Lever Message Shown");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Interact playerInteract = collision.gameObject.GetComponent<LH_Interact>();
			playerInteract.RemoveInteractible(this);
			mCanActivate = false;
			Debug.Log("Player Lever Message Hidden");
		}
	}

	private void Interact() {
		if(mLeverState) {
			// BlockDoor();
			mLeverState = false;
		}
		else {
			// BreakBlocks();
			mLeverState = true;
		}
	}
	
}
