using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_SpikeTrap : DD_Obstacle 
{
	private uint mDamage = 10;

	public void create()
	{
		Debug.Log("Spike Trap Created");
	}

	public void create(uint d)
	{
		mDamage = d;
		Debug.Log("Spike Trap Created with damage " + d);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
			playerHP.doDamage(mDamage);
			Debug.Log("Player damaged");
		}
	}
}
