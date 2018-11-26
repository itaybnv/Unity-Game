using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    //Variables
    public bool destroyAble;
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
    public int health = 100;
    public int armorValue = 1;
    public int armorEffectiveness = 1;


    //Methods
    public void doInteract()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        //health -= (damage) / (armorValue * armorEffectiveness);  // TODO: Fine tune the damage module to work nicely
        health -= damage;
        if (health <= 0)
        {
            health = 0; // in case animation of lowering hp playes don't want -amount of hp
            GameObject.Destroy(gameObject);

        }
    }
}
