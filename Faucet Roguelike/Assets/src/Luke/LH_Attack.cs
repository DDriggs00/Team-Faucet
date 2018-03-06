using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Attack : MonoBehaviour {

	// Use this for initialization

	//AudioManager audio = FindObjectOfType<AudioManager>();
	//LH_Movement movingPlayer = FindObjectOfType<LH_Movement>();
	//Rigidbody2D ridgid

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
        //audio.playSound("swordSwing");
        //OnTriggerEnter2D(movingPlayer.rigidPlayer);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            audio.playSound("swordSwing");
        }
    }
    */
}
