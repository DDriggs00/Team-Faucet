using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Attack : MonoBehaviour {
	// Use this for initialization
    public bool enemyCollision;
    public bool usingWeapon;
    public bool facingEnemy;
    public bool isInvincible;

	void Start () {
        enemyCollision=false;
        usingWeapon=false;
        facingEnemy=false;
        isInvincible=false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
        
    }
    public bool canTakeDamage()
    {
        if(!isInvincible)
        {
           return false;
        }
        else
            return true;
    }
}
