using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Powerup : DD_Obstacle 
{
	private uint mHurtPotionValue = 10;
	private int mHealPotionValue = 10;

	private ObstacleType mPowerupType;	//Amount of HP to heal
	DD_Powerup()
	{
		
		Debug.Log("Random Powerup Created");
		SetObstacleType(ObstacleType.healSpot);
		addObstacle();
	}
	DD_Powerup(bool good)
	{
		
		Debug.Log("Random good Powerup Created");
		SetObstacleType(ObstacleType.healSpot);
		addObstacle();
	}

	DD_Powerup(ObstacleType obsType)
	{
		
		Debug.Log("Powerup Created");
		SetObstacleType(ObstacleType.healSpot);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP;
			switch (mPowerupType)
			{
				case ObstacleType.doubleArmor:
					//Call DoubleArmor
					break;
				case ObstacleType.halfArmor:
					//Call HalfArmor
					break;
				case ObstacleType.doubleDamage:
					break;
				case ObstacleType.halfDamage:
					break;
				case ObstacleType.doubleSpeed:
					break;
				case ObstacleType.halfSpeed:
					break;
				case ObstacleType.healthPotion:
					playerHP = collision.gameObject.GetComponent<LH_Health>();
					playerHP.Heal(mHealPotionValue);
					break;
				case ObstacleType.hurtPotion:
					playerHP = collision.gameObject.GetComponent<LH_Health>();
					playerHP.doDamage(mHurtPotionValue);
					break;
				case ObstacleType.invulnerabilityPotion:
					break;
				default:
					break;
			}

			Destroy(this.gameObject);
		}
	}
}
