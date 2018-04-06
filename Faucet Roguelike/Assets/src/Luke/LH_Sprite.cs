using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LH_Sprite : MonoBehaviour {
    /* the player is going to be a circle for now */

    // Use this for initialization

    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
	void Start () {
		mAnimator = GetComponent<Animator>();
        //mSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0 )
        {
            mAnimator.SetTrigger("westKey");
        }
        else if (Input.GetAxis("Horizontal") > 0 )
        {
            mAnimator.SetTrigger("eastKey");
        }
        else if (Input.GetAxis("Vertical") < 0 )
        {
            mAnimator.SetTrigger("southKey");
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            mAnimator.SetTrigger("northKey");
        }
		else
            mAnimator.SetTrigger("noKey");
	}
	void FixedUpdate()
	{

	}
}
