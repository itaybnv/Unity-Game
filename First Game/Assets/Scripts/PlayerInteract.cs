using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Variables
    public GameObject currentInterObject;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;
    

    //Methods
    public void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInterObject)
        {
            // check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.addItem(currentInterObject);
                
            }
            // check to see if this object is openable and locked
            if (currentInterObjScript.openable)
            {
                string animBool = currentInterObjScript.animBool;
                bool animBoolB = currentInterObjScript.anim.GetBool(currentInterObjScript.animBool);
                if (currentInterObjScript.locked)
                {
                    if (inventory.hasItem(currentInterObjScript.itemNeeded))
                    {
                        // open & play animation
                        currentInterObjScript.anim.SetBool(animBool, !animBoolB);
                        currentInterObjScript.locked = false;
                        //remove item
                        Debug.Log(currentInterObjScript.itemNeeded);
                        inventory.removeItem(currentInterObjScript.itemNeeded);
                        currentInterObjScript.itemNeeded = null;
                    }
                    
                }
                else
                {
                    // open & play animation
                    currentInterObjScript.anim.SetBool(animBool, !animBoolB);
                }
            }
            //Check to see if this object has something to say to you
            if (currentInterObjScript.talks)
            {
                if (currentInterObjScript.itemNeeded)
                {
                    if (inventory.hasItem(currentInterObjScript.itemNeeded))
                    {
                        Debug.Log(currentInterObjScript.message); // say message
                        inventory.removeItem(currentInterObjScript.itemNeeded);
                    }
                    else Debug.Log("You need " + currentInterObjScript.itemNeeded.name + " to talk to " + currentInterObject.name); // player doesn't have the required item to talk to this npc
                }
                else if (currentInterObjScript.message != "")
                {
                    // say the thing the NPC has to say
                    Debug.Log(currentInterObjScript.message);
                }
            }
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject"))
        {
            Debug.Log(collision.name);
            currentInterObject = collision.gameObject;
            currentInterObjScript = currentInterObject.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject") && 
            collision.gameObject == currentInterObject)
        {
            currentInterObject = null;
        }
    }
}
