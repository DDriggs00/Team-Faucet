using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public new string name;
    public enum Type { equip, consumable, misc };
    public Type type;
    public int amount = 1;

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
        if (tag != "Interactable")
        {
            if (!transform.parent.parent.GetComponent<InventoryController>().canDragItem)
                transform.parent.parent.GetComponent<InventoryController>().selectedItem = this.transform;
        }
    }
    private void OnMouseExit()
    {
        if (tag != "Interactable")
        {
            if (!transform.parent.parent.GetComponent<InventoryController>().canDragItem)
                transform.parent.GetComponent<InventoryController>().selectedSlot = null;
        }
    }
    public void IncreaseAmount(int a)
    {
        amount += a;
        transform.Find("count.text").GetComponent<Text>().text = amount.ToString();
    }
}
