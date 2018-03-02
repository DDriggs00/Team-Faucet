using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
	//Variable Declarations
	public int playerDamage;                          

	public float moveSpeed;
	private Vector2 minWalkPoint;
	private Vector3 maxWalkPoint;

	private Rigidbody2D myRigidbody;

	public bool isWalking;

	public float walkTime;
	private float walkCounter;

	public float waitTime;
	private float waitCounter;

	private int WalkDirection;

	public Collider2D walkZone;

	private bool hasWalkZone;
	//** End Variable Declarations ** //




	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();

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
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 1:
				myRigidbody.velocity = new Vector2 (moveSpeed, 0);
				myRigidbody.rotation = 90;
				if (hasWalkZone && transform.position.x > maxWalkPoint.x)
				{
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 2:
				myRigidbody.velocity = new Vector2 (0, -moveSpeed);
				myRigidbody.rotation = 0;
				if (hasWalkZone && transform.position.y < minWalkPoint.y)
				{
					isWalking = false;
					waitCounter = waitTime;

				}
				break;
			case 3:
				myRigidbody.velocity = new Vector2 (-moveSpeed, 0);
				myRigidbody.rotation = 270;
				if (hasWalkZone && transform.position.x < minWalkPoint.x)
				{
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

	public void attackPlayer <T> (T component)
	{


	}


	public void setRoom(Collider2D r)
	{
		walkZone = r;
	}




}