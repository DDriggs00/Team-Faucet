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
        transform.parent.GetComponent<InventoryController>().selectedSlot = null;
    }
}
