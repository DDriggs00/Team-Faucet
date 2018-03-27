using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_SpikeTrap : DD_Obstacle 
{
	private uint mDamage = 10;

//Do on Initialization
	void Start()
	{
		// mDamage = Random(seed + mObstaclesCreated * 5)
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
