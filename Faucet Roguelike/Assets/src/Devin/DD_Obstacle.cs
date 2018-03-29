using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Obstacle : MonoBehaviour
{

	private static int mTotalNumObstacles = 0;

	public enum ObstacleType
	{
		spikeTrap, healSpot, levelExit, pressurePlate
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
}
