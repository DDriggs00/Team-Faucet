using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BOSS - subclass of Enemy for a bigger, slower, and more powerful enemy to come across in the game.
public class Boss : Enemy {

	void Start()
	{
		//Call the initiate function from Enemy superclass
		initiate ();

		//change the difficulty so that Bosses are half as fast and deal 3 times more damage to the Player.
		if (mDifficulty>0)
		{
			mMoveSpeed = (float)mDifficulty/2;
			mPlayerDamage = mDifficulty * 15;
		}
	}
	
	//behave the same as the Enemy superclass by calling updateEnemy();
	void Update () {
		updateEnemy ();
	}
}
