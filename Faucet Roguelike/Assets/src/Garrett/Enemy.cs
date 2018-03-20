using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
	//Variable Declarations
	public int playerDamage;                          

	public float moveSpeed;
	private float moveSpeedX;
	private float moveSpeedY;
	private Vector2 minWalkPoint;
	private Vector3 maxWalkPoint;

	private Rigidbody2D myRigidbody;

	public bool isWalking;

	public float walkTime;
	private float walkCounter;

	public float waitTime;
	private float waitCounter;

	private int WalkDirection;
	private int oldWalkDirection;

	public Collider2D walkZone;

	private bool hasWalkZone;


	private Transform target;
	//** End Variable Declarations ** //




	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		target = GameObject.FindWithTag("Player").transform;

		waitCounter = waitTime;
		walkCounter = walkTime;


		chooseDirection();

		if (walkZone != null)
		{
			minWalkPoint = walkZone.bounds.min;
			maxWalkPoint = walkZone.bounds.max;
			hasWalkZone = true;
		}
			

	}


	void Update()
	{
		if (target)
		{
			attackPlayer ();
		} else
		{
			walk ();
			target = GameObject.FindWithTag("Player").transform;
			if ((target.position.y > maxWalkPoint.y) ||
			    (target.position.x > maxWalkPoint.x) || (target.position.y < minWalkPoint.y) ||
			    (target.position.x < minWalkPoint.x))
			{
				target = null;
			}
		}
	}
		




	private void walk()
	{
		if (isWalking) 
		{
			walkCounter -= Time.deltaTime;


			switch (WalkDirection)
			{
			case 0:
				myRigidbody.velocity = new Vector2 (0, moveSpeed);
				myRigidbody.rotation = 180;
				if (hasWalkZone && transform.position.y > maxWalkPoint.y)
				{
					myRigidbody.velocity = new Vector2 (0, -moveSpeed);
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 1:
				myRigidbody.velocity = new Vector2 (moveSpeed, 0);
				myRigidbody.rotation = 90;
				if (hasWalkZone && transform.position.x > maxWalkPoint.x)
				{
					myRigidbody.velocity = new Vector2 (-moveSpeed, 0);
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 2:
				myRigidbody.velocity = new Vector2 (0, -moveSpeed);
				myRigidbody.rotation = 0;
				if (hasWalkZone && transform.position.y < minWalkPoint.y)
				{
					myRigidbody.velocity = new Vector2 (0, moveSpeed);
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 3:
				myRigidbody.velocity = new Vector2 (-moveSpeed, 0);
				myRigidbody.rotation = 270;
				if (hasWalkZone && transform.position.x < minWalkPoint.x)
				{
					myRigidbody.velocity = new Vector2 (moveSpeed, 0);
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			}

			if (walkCounter < 0)
			{
				isWalking = false;
				waitCounter = waitTime;
			}

		} 
		else
		{
			waitCounter -= Time.deltaTime;

			myRigidbody.velocity = Vector2.zero;

			if (waitCounter < 0)
			{
				chooseDirection ();

			}


		}


	}

	public void chooseDirection()
	{
		WalkDirection = Random.Range (0, 4);
		isWalking = true;
		walkCounter = walkTime;


	}

	public void attackPlayer ()
	{


		if (hasWalkZone)
		{
			if ((transform.position.y > maxWalkPoint.y) ||
				(transform.position.x > maxWalkPoint.x) || (transform.position.y < minWalkPoint.y) ||
				(transform.position.x < minWalkPoint.x))
			{

				target = null;
				return;
			}

		
		}


		if (target.position.x < myRigidbody.position.x)
		{
			moveSpeedX = -moveSpeed;
		} else if (target.position.x > myRigidbody.position.x)
		{
			moveSpeedX = moveSpeed;
		} else
		{
			moveSpeedX = 0;
		}

		if (target.position.y < myRigidbody.position.y)
		{
			moveSpeedY = -moveSpeed;
		} else if (target.position.y > myRigidbody.position.y)
		{
			moveSpeedY = moveSpeed;
		} else
		{
			moveSpeedY = 0;
		}

		myRigidbody.velocity = new Vector2 (moveSpeedX, moveSpeedY);

		//set the rotation
		Vector3 dir = target.position - myRigidbody.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
		myRigidbody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
		


	public void setRoom(Collider2D r)
	{
		walkZone = r;
	}




}