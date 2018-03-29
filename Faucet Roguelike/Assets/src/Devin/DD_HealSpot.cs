using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_HealSpot : DD_Obstacle 
{
	private int mHealing = 10;
	DD_HealSpot()
	{
		Debug.Log("Heal Spot Created");
	}

	DD_HealSpot(int h)
	{
		mHealing = h;
		Debug.Log("Heal Spot Created");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
			playerHP.Heal(mHealing);
			// Debug.Log("Player Health = " + playerHP.getHP());
		}
	}
}
