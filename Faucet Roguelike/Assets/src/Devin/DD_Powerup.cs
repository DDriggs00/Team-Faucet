using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Powerup : DD_Obstacle 
{
	private uint mHurtPotionValue = 10;
	private int mHealPotionValue = 10;
	private int mPositiveModifier = 200;
	private int mNegativeModifier = 50;
	private int mPowerupTime = 30;
	private int mInvincibleTime = 15;

	private ObstacleType mPowerupType;	//Amount of HP to heal
	DD_Powerup()
	{
		
		Debug.Log("Random Powerup Created");
		SetObstacleType(ObstacleType.doubleSpeed);
		addObstacle();
	}
	DD_Powerup(bool good)
	{
		
		Debug.Log("Random good Powerup Created");
		SetObstacleType(ObstacleType.doubleSpeed);
		addObstacle();
	}

	DD_Powerup(ObstacleType obsType)
	{
		
		Debug.Log("Powerup Created");
		SetObstacleType(ObstacleType.halfSpeed);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			LH_Health playerHP;
			LH_Movement playerMovement;
			switch (mPowerupType)
			{
				case ObstacleType.doubleArmor:
					playerHP = collision.gameObject.GetComponent<LH_Health>();
					playerHP.changeArmor(mPositiveModifier);
					break;
				case ObstacleType.halfArmor:
					playerHP = collision.gameObject.GetComponent<LH_Health>();
					playerHP.changeArmor(mNegativeModifier);
					break;
				case ObstacleType.doubleDamage:
					// playerHP = collision.gameObject.GetComponent<LH_Health>();
					// playerHP.setD(mPositiveModifier);
					break;
				case ObstacleType.halfDamage:
					// playerHP = collision.gameObject.GetComponent<LH_Health>();
					// playerHP.changeArmor(mNegativeModifier);
					break;
				case ObstacleType.doubleSpeed:
					playerMovement = collision.gameObject.GetComponent<LH_Movement>();
					playerMovement.changeSpeed(mPositiveModifier);
					break;
				case ObstacleType.halfSpeed:
					playerMovement = collision.gameObject.GetComponent<LH_Movement>();
					playerMovement.changeSpeed(mNegativeModifier);
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
