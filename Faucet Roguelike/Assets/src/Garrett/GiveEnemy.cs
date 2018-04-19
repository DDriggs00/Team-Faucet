using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveEnemy : MonoBehaviour {

	private int newEnemy = 0;
	public Boss mBoss;
	public Minion mMinion;

	// Use this for initialization
	void Start () {


		for(int i=0;i<5;i++)
		{
			generate ();

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void generate()
	{
		
		if(newEnemy==0)
		{
			Minion minionCopy = (Minion)Instantiate(mMinion,new Vector3(1 * 2.0F, 0, 0), Quaternion.identity);
		}
		else
		{
			//Boss bossCopy = (Boss)Instantiate(mBoss, new Vector3(1 * 2.0F, 0, 0), Quaternion.identity);

		}


	}
}
