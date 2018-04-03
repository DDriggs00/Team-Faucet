using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
	//Variable Declarations

	public int mDifficulty;
	private int mPlayerDamage;                          

	private float mMoveSpeed;
	private float mMoveSpeedX;
	private float mMoveSpeedY;
	private Vector2 mMinWalkPoint;
	private Vector3 mMaxWalkPoint;

	private Rigidbody2D mMyRigidBody;

	private bool mIsWalking;

	private float mWalkTime = 0.5f;
	private float mWalkCounter;

	private float mWaitTime = 0.1f;
	private float mWaitCounter;

	private int mWalkDirection;

	public Collider2D mWalkZone;

	private bool mHasWalkZone;


	private Transform mTarget;
	//** End Variable Declarations ** //




	void Start()
	{

		if (mDifficulty!=null)
		{
			mMoveSpeed = mDifficulty;
			mPlayerDamage = mDifficulty * 5;
		}
		mMyRigidBody = GetComponent<Rigidbody2D> ();
		mTarget = GameObject.FindWithTag("Player").transform;

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
		if (mTarget)
		{
			attackPlayer ();
		} else
		{
			walk ();
			mTarget = GameObject.FindWithTag("Player").transform;
			if ((mTarget.position.y > mMaxWalkPoint.y) ||
				(mTarget.position.x > mMaxWalkPoint.x) || (mTarget.position.y < mMinWalkPoint.y) ||
				(mTarget.position.x < mMinWalkPoint.x))
			{
				mTarget = null;
			}
		}
	}





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

	public void chooseDirection()
	{
		mWalkDirection = Random.Range (0, 4);
		mIsWalking = true;
		mWalkCounter = mWalkTime;


	}

	public void attackPlayer ()
	{


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

		mMyRigidBody.velocity = new Vector2 (mMoveSpeedX, mMoveSpeedY);

		//set the rotation
		Vector3 dir = mTarget.position - mMyRigidBody.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
		mMyRigidBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}



	public void setRoom(Collider2D r)
	{
		mWalkZone = r;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		uint damage = (uint)(int)mPlayerDamage;
		if (collision.tag == "Player")
		{
			LH_Health playerHP = collision.gameObject.GetComponent<LH_Health> ();
			playerHP.doDamage (damage);
			Destroy (gameObject);

		}

	}


}
