using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_PressurePlate : DD_Obstacle
{
	private int mTotalCollisions = 0;

	DD_PressurePlate()
	{
		Debug.Log("Pressure Plate Created");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Works regardless of entity
		mTotalCollisions++;
		if(mTotalCollisions == 1) {
			//if there is already something on the pressure plate,
			//pressure plate is already activated
			Debug.Log("PressurePlate Activated");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//Works regardless of entity
		mTotalCollisions--;
		if(mTotalCollisions == 0) {
			//if last object removed
			Debug.Log("Pressure Plate Deactivated");
		}
	}
	
}
