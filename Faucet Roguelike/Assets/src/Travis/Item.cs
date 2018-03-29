using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public new string name;
    public enum Type { equip, consumable, misc };
    public Type type;
    public int amount;

    public Sprite sprite;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if (!transform.parent.parent.GetComponent<InventoryController>().canDragItem)
            transform.parent.parent.GetComponent<InventoryController>().selectedItem = this.transform;
    }
    private void OnMouseExit()
    {
        if (!transform.parent.parent.GetComponent<InventoryController>().canDragItem)
            transform.parent.GetComponent<InventoryController>().selectedSlot = null;
    }
}
