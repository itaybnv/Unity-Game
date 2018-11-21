using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    //Variables
    public bool inventory;  // If true this object can be stored in inventory
    public bool openable;   // If true this object can be opened
    public bool locked;     // If true this object is locked
    public bool talks;      // If true this object can talk to the player

    public GameObject itemNeeded;  // Item needed in order to interact with this object
    public string message;

    public TextAsset textFile;
    public string[] sentences;

    public Animator anim;
    public string animBool = "";

    public int endSentence;
    public int currentSentence;

    //Methods
    public void doInteract()
    {
        gameObject.SetActive(false);
    }
}
