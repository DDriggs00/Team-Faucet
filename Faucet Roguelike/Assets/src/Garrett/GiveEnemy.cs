using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveEnemy : MonoBehaviour {

	//Variable declarations
	private int newEnemy = 0;  //variable to determine which type of enemy to generate in the generate() function.
	public int mDifficulty = 1;  //variable to set the difficulty of the enemies that are spawned in the generate() function
	public GameObject mMinion;
	public GameObject mBoss;
	//End variable declarations

	// Use this for initialization
	void Start () {




		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//This function is used for testing purposes.  It generates an Enemy and will generate either a Minion or a Boss. 
	//There is a 1/4 chance of generating a Boss and a 3/4 chance of generating a Minion.  
	public void generate()
	{
		newEnemy = Random.Range (0, 4);
	
		if(newEnemy==0)
		{
			
			GameObject bossCopy = (GameObject)Instantiate (mBoss, new Vector3 (Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);
			Boss newBoss = bossCopy.GetComponent<Boss> ();

			newBoss.mDifficulty = mDifficulty; //set the difficulty of the new Boss 
		}
		else
		{
			

			GameObject minionCopy = (GameObject)Instantiate (mMinion, new Vector3 (Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);

			Minion newMinion = minionCopy.GetComponent<Minion> ();
			newMinion.mDifficulty = mDifficulty; //set the difficulty of the new Minion

		
		}


	}

	//return a Minion to be spawned in the level
	public GameObject getEnemy()
	{

		return mMinion;



	}

	//return a Boss to be spawned in the level
	public GameObject getBoss()
	{

		return mBoss;


	}
}
