using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Obstacle : MonoBehaviour 
{


	public enum ObstacleType
	{
		spikeTrap, HealSpot, LevelExit, PressurePlate
	};

	private ObstacleType mObsType;

	public ObstacleType GetObstacleType()
	{
		return mObsType;
	}
}
