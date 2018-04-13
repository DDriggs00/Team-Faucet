using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Obstacle : MonoBehaviour
{

	private static int mTotalNumObstacles = 0;
	// private static int mtempSeed = mTotalNumObstacles + 12345;

	public enum ObstacleType
	{
		spikeTrap, healSpot, 
		levelExit, dodoEgg, 
		pressurePlate, lever, 
		doubleDamage, halfDamage,
		doubleArmor, halfArmor,
		doubleSpeed, halfSpeed,
		healthPotion, hurtPotion,
		invulnerabilityPotion
	};

	private ObstacleType mObsType;

	public ObstacleType GetObstacleType()
	{
		return mObsType;
	}
	public void SetObstacleType(ObstacleType obs)
	{
		mObsType = obs;
	}

	public void addObstacle() {
		mTotalNumObstacles++;
	}
	public int getNumObstacles() {
		return mTotalNumObstacles;
	}
}
