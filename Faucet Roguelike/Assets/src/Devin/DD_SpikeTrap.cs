using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a tile that heals the player when they walk on top of it.
public class DD_SpikeTrap : DD_Obstacle 
{
	private uint mDamage = 10;

	DD_SpikeTrap()
	{
		Debug.Log("Spiketrap Created");
		SetObstacleType(ObstacleType.spikeTrap);
		addObstacle();
	}
	DD_SpikeTrap(uint d)
	{
		Debug.Log("Spiketrap Created");
		mDamage = d;
		SetObstacleType(ObstacleType.spikeTrap);
		addObstacle();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
			playerHP.doDamage(mDamage);
			// Debug.Log("Player damaged");
		}
	}
}
