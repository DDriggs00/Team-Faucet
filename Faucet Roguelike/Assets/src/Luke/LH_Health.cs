using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LH_Health : MonoBehaviour {
	private int mHealthPoints = 100;
	private int mHealthCoefficient = 100;
	private int mSecondsCounter = 0;
	private int mMaxSeconds = 10;
	LH_Attack mAttack;
    void Start()
    {
        mAttack = this.GetComponent<LH_Attack>();
    }
	private void changeHealth(int changeAmt)
	{
		if(mAttack.mIsInvincible==false)
		{
			if ( (changeAmt + mHealthPoints) <= 100)
			/*checks to make sure hp stays under 100 */
			{
				mHealthPoints += changeAmt;
			}
			else if ( (changeAmt + mHealthPoints) >= 100)
			{
				mHealthPoints = 100;
			}

			if(mHealthPoints < 0)
			{
				Debug.Log("LH_Health: Player died.");
				mHealthPoints = 100;
			}
		}
		Debug.Log("LH_Health: HP changed to: " + mHealthPoints);

	}
	public void doDamage(uint dmgAmt)
	/*must call with a positive amount for damage */
	{
		int a = Convert.ToInt32(dmgAmt);
		//play sound
		changeHealth( (-a*mHealthCoefficient)/100 );
	}
	public void Heal(int healAmt)
	{
		changeHealth( (healAmt*mHealthCoefficient)/100 );
		//play sound
	}
	public void changeArmor(int percent)  
	{
		mHealthCoefficient = (mHealthCoefficient * percent)/100;
		mSecondsCounter = 0; 
	}
	public void makeInvincible(int seconds)
	{
		mAttack.setInvincible(seconds);
	}
	public int getHP()
	{
		return mHealthPoints;
	}
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
	}
}
