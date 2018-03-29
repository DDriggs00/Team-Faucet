using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    public Transform selectedItem, selectedSlot, originalSlot;

    public GameObject slotPrefab, itemPrefab;
    public Vector2 inventorySize = new Vector2(6, 3);
    public float slotSize;
    public Vector2 windowSize;
    public bool canDragItem = false;

    // Use this for initialization
    void Start()
    {
        for (int x = 1; x <= inventorySize.x; x++)
        {
            for (int y = 1; y <= inventorySize.y; y++)
            {
                GameObject slot = Instantiate(slotPrefab) as GameObject;
                slot.transform.SetParent(this.transform); //make slot child of inv
                slot.name = "slot_" + x + "_" + y; //slot name is x and y value
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(windowSize.x / (inventorySize.x + 1) * x, windowSize.y / (inventorySize.y + 1) * -y, 0);

                if (y <= GameDB.itemList.Count)
                {

                    GameObject item = Instantiate(itemPrefab) as GameObject;
                    item.transform.SetParent(slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    Item i = item.GetComponent<Item>();

                    i.name = GameDB.itemList[(x + (y - 1) * 6) - 1].name;
                    i.type = GameDB.itemList[(x + (y - 1) * 6) - 1].type;
                    i.sprite = GameDB.itemList[(x + (y - 1) * 6) - 1].sprite;

                    item.name = i.name;
                    item.GetComponent<Image>().sprite = i.sprite;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedItem != null)
        {
            canDragItem = true;
            originalSlot = selectedItem.parent;
            selectedItem.GetComponent<Collider>().enabled = false;

        }
        if (Input.GetMouseButton(0) && selectedItem != null && canDragItem)//If left click and keep pressed
        {
            selectedItem.position = Input.mousePosition; //selected Item follows mouse
        }
        else if (Input.GetMouseButtonUp(0) && selectedItem != null) // release the mouse button
        {
            canDragItem = false;

            if (selectedSlot == null) selectedItem.SetParent(originalSlot); //if no selected parent is original
            else
            {
                if (selectedSlot.childCount > 0)
                { //Stackable Items
                    if (selectedItem.name == selectedSlot.GetChild(0).name &&
                       (selectedItem.GetComponent<Item>().type == Item.Type.consumable || //stackable items
                       (selectedItem.GetComponent<Item>().type == Item.Type.misc)))       //stackable items
                    {
                        Debug.Log("We stacked 2 items");
                    }
                    //Swappable Items  
                    else
                    {
                       selectedSlot.GetChild(0).SetParent(originalSlot);
                       foreach (Transform t in originalSlot) t.localPosition = Vector3.zero;
                    }
                }
                selectedItem.SetParent(selectedSlot); //parent is selected slot
            }
            selectedItem.localPosition = Vector3.zero; //selected Item goes back to origin
            selectedItem.GetComponent<Collider>().enabled = true;
        }

    }

}
