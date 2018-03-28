using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Attack : MonoBehaviour {
	// Use this for initialization
    public bool mEnemyCollision;
    public bool mUsingWeapon;
    public bool mFacingEnemy;
    public bool mIsInvincible;

	void Start () {
        mEnemyCollision=false;
        mUsingWeapon=false;
        mFacingEnemy=false;
        mIsInvincible=false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
        
    }
    public bool canTakeDamage()
    {
        if(!mIsInvincible)
        {
           return false;
        }
        else
            return true;
    }
}
