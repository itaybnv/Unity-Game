using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //Variables
    public GameObject[] inventory = new GameObject[10];

	//Methods
    public void addItem(GameObject item)
    {
        bool itemAdded = false;
        //Find the first open slot in the inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log(item.name + " Added to inventory");
                itemAdded = true;
                item.SendMessage("doInteract");  
                break;
            }
        }
        //Inventory is full
        if (!itemAdded)
        {
            Debug.Log(item.name + "Can't be added, inventory full");
        }
    }
    
    public bool hasItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public void removeItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            Debug.Log(item + "      " + inventory[i]);
            Debug.Log("");
            if(inventory[i] == item)
            {
                inventory[i] = null;
                break;
            }
        }
    }
}
