using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LH_Health : MonoBehaviour {
	private int healthPoints;
	private void changeHealth(int changeAmt)
	{
		if ( (changeAmt + healthPoints) <= 100)
		/*checks to make sure hp stays under 100 */
		{
			healthPoints += changeAmt;
		}
	}
	public void doDamage(uint dmgAmt)
	/*must call with a positive amount for damage */
	{
		int a = Convert.ToInt32(dmgAmt);
		changeHealth(-a);
	}
	public void Heal(int healAmt)
	{
		changeHealth(healAmt);
	}
	public int getHP()
	{
		return healthPoints;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
