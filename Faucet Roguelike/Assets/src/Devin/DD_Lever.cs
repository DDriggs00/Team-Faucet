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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if(collision.tag == "Player")
		{
			// LH_Interact playerInteract = collision.gameObject.GetComponent<LH_Interact>();
			// playerInteract.showInteraction(this);
			mCanActivate = true;
			Debug.Log("Player Lever Message Shown");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			// LH_Health playerInteract = collision.gameObject.GetComponent<LH_Interact>();
			// playerInteract.hideInteraction(this);
			mCanActivate = false;
			Debug.Log("Player Lever Message Hidden");
		}
	}

	private void Interact() {
		
	}
	
}
