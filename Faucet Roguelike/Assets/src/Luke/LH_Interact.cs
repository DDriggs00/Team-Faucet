using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Currently, DD_Lever is the only interactible object.
//Will be changed later to a more appropriate superclass.

public class LH_Interact : MonoBehaviour {

	// Use this for initialization
	private List<DD_Lever> mInteractibleObjects;
	private bool mIsInteracting;

	DD_Lever mInteractible;
	void Start () 
	{
		mInteractible=FindObjectOfType<DD_Lever>();
		mInteractibleObjects = null; //start with empty list
		mIsInteracting = false; //no interaction at game start
	}

	void Update()
	{
		if( Input.GetButton("Fire3") )
		{
			if(mInteractibleObjects.Count >0)
			{
				mIsInteracting = true;
			}
			else
			{
				Debug.Log("No interactible objects nearby");
				mIsInteracting = false;
			}
		}
		else
			mIsInteracting = false;
	}

	public void AssignInteractible(DD_Lever addingObject)
	{
		mInteractibleObjects.Add(addingObject);
		mInteractible=mInteractibleObjects[0];
			//Currently only interacts with the first objects in the list
			//Will soon interact with the closest object
		return;
	}
	public void RemoveInteractible(DD_Lever removingObject)
	{
		mInteractibleObjects.Remove(removingObject);
		//removes first instance of matching type
		return;
	}

	public bool IsInteracting()
	{
		return mIsInteracting;
	}

	/*
	public List<DD_Lever> GetInteractingList()
	{
		return mInteractibleObjects;
	}
	*/

	private void Interact()
	{
		if (mIsInteracting == true)
		{
			Debug.Log("Calling Devin's interaction function");
			//mInteractible.Interact();
				//Devin, can you make this public?
		}
	}
}
