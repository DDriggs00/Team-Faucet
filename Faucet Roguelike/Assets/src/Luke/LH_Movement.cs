using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Movement : MonoBehaviour 
{
	public Rigidbody2D rigidPlayer;
	public int speedCoefficient=5;
    Vector2 userInput;


    // Use this for initialization
    void Start () 
	{
		//Debug.Log("The Player is here!");
        rigidPlayer = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update () 
	{
        userInput = GetMoveInput();
    }

    Vector2 GetMoveInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        return new Vector2(h, v);
    }

    void FixedUpdate()
	{
        movePlayer(userInput);
	}

    public void movePlayer(Vector2 dir)
	{
        dir.Normalize();
		rigidPlayer.velocity=dir*speedCoefficient;
	}
}

