using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Attack : MonoBehaviour {

	// Use this for initialization

	AudioManager audio = FindObjectOfType<AudioManager>();
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
        //LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
		//Enemy gameEnemy = collision
	
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            audio.playSound("swordSwing");
			//collision.gameObject.GetComponent<Enemy>
			//LH_Health playerHP = collision.gameObject.GetComponent<LH_Health>();
            //playerHP.doDamage(mDamage);
            // Debug.Log("Player damaged");
        }
    }
}
