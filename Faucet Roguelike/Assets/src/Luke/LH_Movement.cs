using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Movement : MonoBehaviour 
{
	public Rigidbody2D mRigidPlayer;
	public int mSpeedCoefficient=5;
    Vector2 mUserInput;


    // Use this for initialization
    void Start () 
	{
		//Debug.Log("The Player is here!");
        mRigidPlayer = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update () 
	{
        mUserInput = GetMoveInput();
    }

    Vector2 GetMoveInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        return new Vector2(h, v);
    }

    void FixedUpdate()
	{
        movePlayer(mUserInput);
	}

    public void movePlayer(Vector2 dir)
	{
        dir.Normalize();
		mRigidPlayer.velocity=dir*mSpeedCoefficient;
	}
}

