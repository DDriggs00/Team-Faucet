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
		if(collision.tag == "Player") // If player touches egg (ignore mobs)
		{
			FindObjectOfType<ZG_AudioManager>().playFixedSound("gameOver"); // No Dodo egg sound yet
			// Commented code is not implemented by teammates
			// bool mGameOver = WinGui();
			// if(mGameOver)
			// {
				Application.Quit(); //Ends the game
			// }
			// else {
			// 	SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//reload scene
			// 	Debug.Log("Level Ended");
			// }
		}
	}
}
