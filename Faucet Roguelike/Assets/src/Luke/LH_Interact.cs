using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Currently, DD_Lever is the only interactible object.
//Will be changed later to a more appropriate superclass.

public class LH_Interact : MonoBehaviour
{

    // Use this for initialization
    private List<DD_Lever> mInteractibleObjects;
    private bool mIsInteracting;

    DD_Lever mInteractible;
    void Start()
    {
        mInteractible = FindObjectOfType<DD_Lever>();
        mInteractibleObjects = null; //start with empty list
        mIsInteracting = false; //no interaction at game start
    }

    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            if (mInteractibleObjects.Count > 0)
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
        mInteractible = mInteractibleObjects[0];
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

    //Travis test for collisons with objects
    int x, y = 1;
    public GameObject slotPrefab, itemPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            print("we picked up " + other.name);
            GameDB._instance.AddItem(other.GetComponent<Item>()); //delegation of _instance from GameDB observer pattern add remove notify
            GameObject.Destroy(other.gameObject, 0.1f);           // DESTROY GAME OBJECT AFTER PICKUP
            /*GameDB.AddItem(other);                             
            if (y <= GameDB.itemList.Count)
            {

                GameObject slot = Instantiate(slotPrefab) as GameObject;
                slot.transform.SetParent(this.transform); //make slot child of inv
                slot.name = "slot_" + x + "_" + y; //slot name is x and y value

                GameObject item = Instantiate(itemPrefab) as GameObject;
                item.transform.SetParent(slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                Item i = item.GetComponent<Item>();

                i.name = GameDB.itemList[(x + (y - 1) * 6) - 1].name;
                i.type = GameDB.itemList[(x + (y - 1) * 6) - 1].type;
                i.sprite = GameDB.itemList[(x + (y - 1) * 6) - 1].sprite;

                item.name = i.name;
                item.GetComponent<Image>().sprite = i.sprite;

            }*/
        }
    }
}