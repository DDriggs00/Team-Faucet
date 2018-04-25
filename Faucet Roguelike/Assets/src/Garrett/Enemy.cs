using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
	//Variable Declarations

	public int mDifficulty;
	protected int mPlayerDamage; // amount of damage the Enemy may do to the player based on the difficulty setting.                          

	protected float mMoveSpeed;  // general speed that the Enemy may move based on the difficulty setting.
	private float mMoveSpeedX;  // X component of mMoveSpeed
	private float mMoveSpeedY;  // Y component of mMoveSpeed
	private Vector2 mMinWalkPoint;  //variables to determine if the Player has stepped outside of the Enemy's boundary.
	private Vector3 mMaxWalkPoint;

	private Rigidbody2D mMyRigidBody;

	//variables for the walk() function
	private bool mIsWalking;
	private float mWalkTime = 0.5f;
	private float mWalkCounter;
	private float mWaitTime = 0.1f;
	private float mWaitCounter;
	private int mWalkDirection;

	public Collider2D mWalkZone;  //variable to define the boundary that the Enemies may be active in

	private bool mHasWalkZone;


	private Transform mTarget;
	//** End Variable Declarations ** //




	void Start()
	{

		initiate ();


	}



	//initiate function: allows subclasses to start the same way.
	protected virtual void initiate()
	{

		if (mDifficulty>0)
		{
			mMoveSpeed = mDifficulty;
			mPlayerDamage = mDifficulty * 5;
		}
		mMyRigidBody = GetComponent<Rigidbody2D> ();
		mTarget = GameObject.FindWithTag("Player").transform;
		if (mTarget)
		{
			FindObjectOfType<ZG_AudioManager>().playDynamicSound("ememyPursuit1");

		}

		mWaitCounter = mWaitTime;
		mWalkCounter = mWalkTime;


		chooseDirection();

		if (mWalkZone != null)
		{
			mMinWalkPoint = mWalkZone.bounds.min;
			mMaxWalkPoint = mWalkZone.bounds.max;
			mHasWalkZone = true;
		}


	}


	void Update()
	{
		updateEnemy ();
	}



	//function that allows subclasses to update the same as the superclass
	protected virtual void updateEnemy()
	{
		if (mTarget)
		{
			attackPlayer ();
			//lose interest in the Player if the Player leaves the Enemy's boundary
			if ((mTarget.position.y > mMaxWalkPoint.y) ||
				(mTarget.position.x > mMaxWalkPoint.x) || (mTarget.position.y < mMinWalkPoint.y) ||
				(mTarget.position.x < mMinWalkPoint.x))
			{
				mTarget = null;
			}
		} else
		{
			// wander around aimlessly until a target (the Player) is acquired.
			walk ();
			mTarget = GameObject.FindWithTag("Player").transform;
			if (mTarget)
			{
				FindObjectOfType<ZG_AudioManager>().playDynamicSound("ememyPursuit1");
			}

			//prevent enemy from acquiring an interest in the Player if the Player is outside the Enemy's boundary
			if ((mTarget.position.y > mMaxWalkPoint.y) ||
				(mTarget.position.x > mMaxWalkPoint.x) || (mTarget.position.y < mMinWalkPoint.y) ||
				(mTarget.position.x < mMinWalkPoint.x))
			{
				mTarget = null;
			}
		}



	}



	//defines how the enemies wander around when there is no Player to chase
	private void walk()
	{
		if (mIsWalking) 
		{
			mWalkCounter -= Time.deltaTime;


			switch (mWalkDirection)
			{
			case 0:
				mMyRigidBody.velocity = new Vector2 (0, mMoveSpeed);
				mMyRigidBody.rotation = 180;
				if (mHasWalkZone && transform.position.y > mMaxWalkPoint.y)
				{
					mMyRigidBody.velocity = new Vector2 (0, -mMoveSpeed);
					mIsWalking = false;
					mWaitCounter = mWaitTime;

				}
				break;
			case 1:
				mMyRigidBody.velocity = new Vector2 (mMoveSpeed, 0);
				mMyRigidBody.rotation = 90;
				if (mHasWalkZone && transform.position.x > mMaxWalkPoint.x)
				{
					mMyRigidBody.velocity = new Vector2 (-mMoveSpeed, 0);
					mIsWalking = false;
					mWaitCounter = mWaitTime;

				}
				break;
			case 2:
				mMyRigidBody.velocity = new Vector2 (0, -mMoveSpeed);
				mMyRigidBody.rotation = 0;
				if (mHasWalkZone && transform.position.y < mMinWalkPoint.y)
				{
					mMyRigidBody.velocity = new Vector2 (0, mMoveSpeed);
					mIsWalking = false;
					mWaitCounter = mWaitTime;

				}
				break;
			case 3:
				mMyRigidBody.velocity = new Vector2 (-mMoveSpeed, 0);
				mMyRigidBody.rotation = 270;
				if (mHasWalkZone && transform.position.x < mMinWalkPoint.x)
				{
					mMyRigidBody.velocity = new Vector2 (mMoveSpeed, 0);
					mIsWalking = false;
					mWaitCounter = mWaitTime;

				}
				break;
			}

			if (mWalkCounter < 0)
			{
				mIsWalking = false;
				mWaitCounter = mWaitTime;
			}

		} 
		else
		{
			mWaitCounter -= Time.deltaTime;

			mMyRigidBody.velocity = Vector2.zero;

			if (mWaitCounter < 0)
			{
				chooseDirection ();

			}


		}


	}


	//simple function to choose a random direction to move in the walk() function
	public void chooseDirection()
	{
		mWalkDirection = Random.Range (0, 4);
		mIsWalking = true;
		mWalkCounter = mWalkTime;


	}


	//function to chase the Player
	public void attackPlayer ()
	{

		//if the Enemy has a defined boundary, lose interest in the Player if the Player leaves that boundary.
		if (mHasWalkZone)
		{
			if ((transform.position.y > mMaxWalkPoint.y) ||
				(transform.position.x > mMaxWalkPoint.x) || (transform.position.y < mMinWalkPoint.y) ||
				(transform.position.x < mMinWalkPoint.x))
			{

				mTarget = null;
				return;
			}
		}

		//code to determine the Enemy's direction of movement relative to the Player's position
		if (mTarget.position.x < mMyRigidBody.position.x)
		{
			mMoveSpeedX = -mMoveSpeed;
		} else if (mTarget.position.x > mMyRigidBody.position.x)
		{
			mMoveSpeedX = mMoveSpeed;
		} else
		{
			mMoveSpeedX = 0;
		}

		if (mTarget.position.y < mMyRigidBody.position.y)
		{
			mMoveSpeedY = -mMoveSpeed;
		} else if (mTarget.position.y > mMyRigidBody.position.y)
		{
			mMoveSpeedY = mMoveSpeed;
		} else
		{
			mMoveSpeedY = 0;
		}

		//set the velocity based on the speed values for X and Y that were just determined.
		mMyRigidBody.velocity = new Vector2 (mMoveSpeedX, mMoveSpeedY);


		//REUSED CODE - user filipst on StackOverflow
		//rotate the Enemy to face the Player
		Vector3 dir = mTarget.position - mMyRigidBody.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
		mMyRigidBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		//END REUSED CODE

	}


	//sets the boundary that the Enemies can be active in. If nothing is set, the enemies may wander anywhere.
	public void setRoom(Collider2D r)
	{
		mWalkZone = r;
	}


	//Upon colliding the Player, respond by dealing damage to the Player and dying
	//This functions as a very basic fighting mechanic
	private void OnTriggerEnter2D(Collider2D collision)
	{

		uint damage = (uint)(int)mPlayerDamage;
		if (collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health> ();
			FindObjectOfType<ZG_AudioManager>().playDynamicSound("enemyTakeDamage1");
			playerHP.doDamage (damage);
			Destroy (gameObject);

		}

	}


}
