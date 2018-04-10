using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// For the ability to reset the level

public class DD_LevelExit : DD_Obstacle 
{
	private bool mUnlocked = true;	// Whether exit can be used
	private bool mFinalExit = false;
	// private static int mNumExits = 0;

	DD_LevelExit()
	{	
		// if(mNumExits >= 3) {
		// 	mFinalExit = true;
		// 	Debug.Log("FINAL Exit Created");
		// }
		Debug.Log("Level Exit Created");
		SetObstacleType(ObstacleType.levelExit);
		addObstacle(); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//prompt to exit
		//save

		if(collision.tag == "Player" && mUnlocked) //if Exit is unlocked and player is colliding
		{
			// if(mFinalExit) {
			// 	//Finish game, show score
			// }
			// else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//reload scene
				Debug.Log("Level Ended");
			// }
		}
	}
}
