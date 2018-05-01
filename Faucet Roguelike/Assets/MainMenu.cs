// Zach Moreno
// Unity Main Menu
// Hunt for the Dodo Egg 2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	
	public void playGame()
    {
        Debug.Log("START!");
        // Call Alex's function for random level generation, Level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadGame()
    {
        // Call Drew's function to load a previosuly saved game
        Debug.Log("LOAD!");
    }

    public void quitGame()
    {
        Debug.Log("QUIT!");
        // Universal call in Unity to exit from application
        Application.Quit();
    }
}
