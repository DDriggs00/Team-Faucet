using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Movement : MonoBehaviour 
{
<<<<<<< HEAD
	Rigidbody2D rigid;
=======
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);
    }
	public Rigidbody2D rigidPlayer;
	public int speedCoefficient=5;
>>>>>>> fe6bda252a57f0a0552eef40f77c8001d8540a7b

 

	// Use this for initialization
	void Start () 
	{
		//Debug.Log("The Player is here!");

        rigid = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		

	}
	void FixedUpdate()
	{
        /* moves player */
		if (Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector2(-150 * Time.fixedDeltaTime, 0);
        }
		else if (Input.GetKey(KeyCode.D))
		{
			rigid.velocity = new Vector2(150 * Time.fixedDeltaTime, 0);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rigid.velocity = new Vector2(0, -150 * Time.fixedDeltaTime);
		}
		else if (Input.GetKey(KeyCode.W))
		{
			rigid.velocity = new Vector2(0, 150 * Time.fixedDeltaTime );
		}
        else
            rigid.velocity = new Vector2(0, 0);
	}

}

