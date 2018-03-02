
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private bool attacking = false; //initialize attack to off
    private float attackTimer = 0;  
    private float attackCd = 0.3f;  //attack cooldown timer
    public Collider2D attackTrigger; 

    private void Awake()
    {
        attackTrigger.enabled = false; //start with attack trigger off
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !attacking) //when spacebar is pressed and were not currently attacking
        {
            print("attack was successful"); //used for testing purposes
            Debug.Log("The Player Attacked"); //used for testing purposes
            attacking = true; //attack
            attackTimer = attackCd; // apply cooldown

            attackTrigger.enabled = true; //apply trigger
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime; //reduce timer by seconds passed
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }
    }
}
