using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LH_Health : MonoBehaviour {
	private int healthPoints = 100;
	LH_Attack attack;
    void Start()
    {
        attack = this.GetComponent<LH_Attack>();
    }
	private void changeHealth(int changeAmt)
	{
		if(attack.isInvincible==false)
		{
			if ( (changeAmt + healthPoints) <= 100)
			/*checks to make sure hp stays under 100 */
			{
				healthPoints += changeAmt;
			}
			else if ( (changeAmt + healthPoints) >= 100)
			{
				healthPoints = 100;
			}

			if(healthPoints < 0)
			{
				Debug.Log("LH_Health: Player died.");
				healthPoints = 100;
			}
		}
		Debug.Log("LH_Health: HP changed to: " + healthPoints);

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
		return healthPoints;
	}
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
	}
}
