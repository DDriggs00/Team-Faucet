using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LH_Attack : MonoBehaviour {
	// Use this for initialization
    private int mCurrentSeconds;
    private int mMaxSeconds;
    private float mPrevTime;
    private float mCurrTime;
    public bool mEnemyCollision;
    public bool mUsingWeapon;
    public bool mFacingEnemy;
    public bool mIsInvincible;

	void Start () {
        resetCounters();
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
        //set up a counter here to count seconds
        if (mIsInvincible == true)
        {
            mCurrTime = Time.fixedTime;
            mCurrentSeconds = Convert.ToInt32(mCurrTime-mPrevTime);
        }
        if (mCurrentSeconds > mMaxSeconds)
        {
            mIsInvincible = false;
            resetCounters();
        }
            
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
    public void setInvincible(int seconds)
    {
        mIsInvincible = true;
        mMaxSeconds = seconds;
        mPrevTime = Time.fixedTime;
    }
    private void resetCounters()
    {
        mCurrentSeconds = 0;
        mMaxSeconds = 0;
        mPrevTime = -1;
        mCurrTime = -1;
    }
}
