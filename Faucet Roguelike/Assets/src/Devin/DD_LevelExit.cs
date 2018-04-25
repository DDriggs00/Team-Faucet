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
		// Prompt to exit Gui not completed
		

		if(collision.tag == "Player" && mUnlocked) //if Exit is unlocked and player is colliding
		{
			// AudioManager mSound;
			FindObjectOfType<ZG_AudioManager>().playFixedSound("gameOver"); // Play Zane's sound
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);	// Reload scene
			Debug.Log("Level Ended");
		}
	}
}
