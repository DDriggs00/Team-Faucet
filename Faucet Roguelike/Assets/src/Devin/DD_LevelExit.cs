using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// For the ability to reset the level

public class DD_LevelExit : DD_Obstacle 
{
	private bool mUnlocked = true;	// Whether exit can be used

	DD_LevelExit()
	{	
		Debug.Log("Level Exit Created");
		SetObstacleType(ObstacleType.levelExit);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Prompt to exit
		// Save

		if(collision.tag == "Player" && mUnlocked) //if Exit is unlocked and player is colliding
		{
			// AudioManager mSound;
			// mSound.playSound("levelFinish");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//reload scene
			Debug.Log("Level Ended");
		}
	}
}
