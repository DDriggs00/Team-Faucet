using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject inventory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
		
	}

    void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        Cursor.visible = inventory.activeInHierarchy;
    }
}
