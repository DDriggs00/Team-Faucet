using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DD_LevelExit : DD_Obstacle 
{
	bool mUnlocked = true;		//for showing the unlock status of the exit

	DD_LevelExit()
	{	
		Debug.Log("LevelExit Created");
		// SetObstacleType(LevelExit); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//prompt to exit
		//save

		if(collision.tag == "Player" && mUnlocked) //if Exit is unlocked and player is colliding
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//reload scene
			Debug.Log("Level Ended");
		}
	}
}
