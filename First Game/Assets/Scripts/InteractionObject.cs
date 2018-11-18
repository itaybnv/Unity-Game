using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    //Variables
    public bool inventory;  // If true this object can be stored in inventory
    public bool openable;   // If true this object can be opened
    public bool locked;     // If true this object is locked
    public bool talks;      // If true this object can talk to the player

    public GameObject itemNeeded;  // Item needed in order to interact with this object
    public string message;         // The message this object will give the player

    public Animator anim;
    public string animBool = "";

    //Methods

    public void doInteract()
    {
        gameObject.SetActive(false);
    }


}
