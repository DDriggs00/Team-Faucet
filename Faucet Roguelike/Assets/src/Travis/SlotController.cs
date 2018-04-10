using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{

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
         transform.parent.GetComponent<InventoryController>().selectedSlot = this.transform;
    }

    private void OnMouseExit()
    {
        if (!transform.parent.parent.GetComponent<InventoryController>().canDragItem)
            transform.parent.GetComponent<InventoryController>().selectedSlot = null;
    }
}
