using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {


	// Use this for initialization
	void Start()
	{
		initiate ();
		if (mDifficulty>0)
		{
			mMoveSpeed = (float)mDifficulty/2;
			mPlayerDamage = mDifficulty * 15;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateEnemy ();
	}
}
