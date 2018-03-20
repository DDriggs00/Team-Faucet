/* 
 *	AP_PopItem class used to coordinate between procedurally generated items and room populator classes
 * item classes returns a AP_PopItem object with an array of items to be placed and whether or not they should be adjacent

To create a pop item in another class/function
 		AP_PopItem popItems = new AP_PopItem ();
InitItems can then be used to declare how many items you want to assign;
		popItems.InitItems(3);
You can then assign each item individually using SetItem
		popItems.SetItem(0, someGameObject);
Alternatively, you can initialize and set the entire Items array using SetItemsArray
		popItems.SetItemsArray(someGameObjectArray);

 */

using UnityEngine;

public class AP_PopItem {

	GameObject[] Items;

	public void SetItemsArray(GameObject[] objArr)
	{
		int SIZE = objArr.Length;
		InitItems (SIZE);
	
		for(int i = 0; i < SIZE; i++)
		{
			SetItem(i, objArr[i]);
		}
	}

	public void InitItems(int SIZE)
	{
		Items = new GameObject[SIZE];
	}

	public void SetItem(int index, GameObject obj)
	{
		if (index >= Items.Length)
			Debug.Log ("Failed to Add Item - Index is out of Item array bounds");
		else
		{
			Items [index] = obj;
		}
			
	}



}


