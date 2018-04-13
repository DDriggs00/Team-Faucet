using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// For the ability to reset the level

public class DD_DodoEgg : DD_Obstacle 
{
	DD_DodoEgg()
	{	
		Debug.Log("THE GOLDEN DODO EGG HAS BEEN LAID");
		SetObstacleType(ObstacleType.dodoEgg);
		addObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player") //if player is colliding
		{
			// bool mGameOver = WinGui();
			// if(mGameOver)
			// {
				Application.Quit();
			// }
			// else {
			// 	SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//reload scene
			// 	Debug.Log("Level Ended");
			// }
		}
	}
}
