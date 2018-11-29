using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //Variables
    public GameObject[] inventory = new GameObject[10];

    public GameObject InventoryPanel;
    public Image[] inventoryImages = new Image[6];

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
                itemAdded = true;
                inventoryImages[i].sprite = item.GetComponent<SpriteRenderer>().sprite;
                item.SendMessage("doInteract");
                break;
            }
        }
        //Inventory is full
        if (!itemAdded)
        {
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
            if(inventory[i] == item)
            {
                inventory[i] = null;
                inventoryImages[i].sprite = null;
                break;
            }
        }
    }
}
