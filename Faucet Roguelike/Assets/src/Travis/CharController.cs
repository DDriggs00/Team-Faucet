using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            print("we picked up " + other.name);
            GameDB._instance.AddItem(other.GetComponent<Item>()); //delegation of _instance from GameDB observer pattern add remove notify
         // GameObject.Destroy(other.gameObject, 0.1f);           // DESTROY GAME OBJECT AFTER PICKUP it now destroys player...
         // GameDB.AddItem(other);                                //instead of adding item to list it adds player...
        }
    }

}

