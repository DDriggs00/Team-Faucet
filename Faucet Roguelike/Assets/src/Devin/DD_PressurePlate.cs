using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_PressurePlate : DD_Obstacle
{
	public void create()
	{
		Debug.Log("Pressure Plate Created");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Works regardless of entity
		Debug.Log("PressurePlate Activated");
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//Works regardless of entity
		Debug.Log("Pressure Plate Deactivated");

	}
}
