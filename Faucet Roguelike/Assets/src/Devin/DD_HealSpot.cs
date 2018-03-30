using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_HealSpot : DD_Obstacle 
{
	private int mHealing;	//Amount of HP to heal
	DD_HealSpot()
	{
		mHealing = 50;
		Debug.Log("Heal Spot Created");
		SetObstacleType(ObstacleType.healSpot);
		addObstacle();
	}

	DD_HealSpot(int h)
	{
		mHealing = h;
		Debug.Log("Heal Spot Created");
		SetObstacleType(ObstacleType.healSpot);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
			playerHP.Heal(mHealing);
			// Destroy(this.gameObject);
		}
	}
}
