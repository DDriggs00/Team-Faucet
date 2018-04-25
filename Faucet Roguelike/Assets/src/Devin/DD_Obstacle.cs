// Use Instructions: All obstacles should be generated through
// the use of the DD_GenObstacle class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Obstacle : MonoBehaviour
{
	private const int MAX_LEVEL = 3;

	private static int mTotalNumObstacles = 0;
	private static int mLevelNumber = 0;

	public enum ObstacleType // Type to indicate type of obstacle
	{
		spikeTrap, healSpot, levelExit, dodoEgg, pressurePlate, lever, 
		doubleDamage, halfDamage, doubleArmor, halfArmor, doubleSpeed, 
		halfSpeed, healthPotion, hurtPotion, invulnerabilityPotion
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

	public int getLevelNumber() {
		return mLevelNumber;
	}
	public void nextLevel() {
		mLevelNumber++;
	}
	public int getMaxLevel() {
		return MAX_LEVEL;
	}
}
