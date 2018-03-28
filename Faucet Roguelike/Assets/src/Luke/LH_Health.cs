using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LH_Health : MonoBehaviour {
	private int mHealthPoints = 100;
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
		changeHealth(-a);
	}
	public void Heal(int healAmt)
	{
		changeHealth(healAmt);
		//play sound
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
