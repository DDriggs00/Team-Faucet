using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveEnemy : MonoBehaviour {

	private int newEnemy = 0;
	public int mDifficulty = 1;
//	public Boss mBoss;
//	public Minion mMinion;
	public GameObject mMinion;
	public GameObject mBoss;
	private GameObject newtBoss;

	// Use this for initialization
	void Start () {




		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void generate()
	{
		newEnemy = Random.Range (0, 4);
	
		if(newEnemy==0)
		{

//				Boss bossCopy = (Boss)Instantiate(mBoss, new Vector3(Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);
			GameObject bossCopy = (GameObject)Instantiate (mBoss, new Vector3 (Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);
			Boss newBoss = bossCopy.GetComponent<Boss> ();

			newBoss.mDifficulty = mDifficulty;
		}
		else
		{
			
//			Minion minionCopy = (Minion)Instantiate (mMinion, new Vector3 (Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);
			GameObject minionCopy = (GameObject)Instantiate (mMinion, new Vector3 (Random.Range (0, 4) * 2.0F, 0, 0), Quaternion.identity);

			Minion newMinion = minionCopy.GetComponent<Minion> ();
			newMinion.mDifficulty = mDifficulty;

		
		}


	}


	public GameObject getEnemy()
	{

		return mMinion;



	}


	public GameObject getBoss()
	{

		return mBoss;


	}
}
