using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //Variables
    public GameObject currentInterObject;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    public GameObject dialoguePanel;
    public Text dialogueText;
    private int currentSentenceOriginal;

    public Text notificationText;
    public Image notificationImage;
    public Animator notificationAnim;

    public Player playerScript;

    public InputField inputField;
    public Text chatContent;
    public LinkedList<string> chatMessages = new LinkedList<string>();

    //Methods
    public void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInterObject)
        {
            // check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.addItem(currentInterObject);

                //notification
                notificationText.text = currentInterObject.name + " picked up!";
                notificationAnim.SetTrigger("IsNotification");
                currentInterObject = null;

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
                        inventory.removeItem(currentInterObjScript.itemNeeded);
                        currentInterObjScript.itemNeeded = null;

                        //notification
                        notificationText.text = currentInterObject.name + " unlocked and opened";
                        notificationAnim.SetTrigger("IsNotification");

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
                    dialoguePanel.SetActive(true);
                    if (inventory.hasItem(currentInterObjScript.itemNeeded))
                    {

                        //dialogueText.text = currentInterObjScript.message; // say message
                        //dialogueText.text = currentInterObjScript.sentences[currentInterObjScript.currentSentence];
                        StopAllCoroutines();
                        StartCoroutine(TypeSentence(currentInterObjScript.sentences[currentInterObjScript.currentSentence]));
                        currentInterObjScript.currentSentence++;

                        inventory.removeItem(currentInterObjScript.itemNeeded);

                        //notification
                        notificationText.text = currentInterObjScript.itemNeeded + " removed from inventory";
                        notificationAnim.SetTrigger("IsNotification");
                        currentInterObjScript.itemNeeded = null;  // 'unlocks' the NPC to the next dialogue interactions

                    }
                    else dialogueText.text = "You need " + currentInterObjScript.itemNeeded.name + " to talk to " + currentInterObject.name; // player doesn't have the required item to talk to this npc
                }
                else if (currentInterObjScript.sentences != null)
                {
                    //dialogueText.text = currentInterObjScript.message; // say message
                    //dialogueText.text = currentInterObjScript.sentences[currentInterObjScript.currentSentence];
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(currentInterObjScript.sentences[currentInterObjScript.currentSentence]));
                    currentInterObjScript.currentSentence++;
                    dialoguePanel.SetActive(true);
                }
                // Loop back to the first sentence when reached the end
                if (currentInterObjScript.currentSentence == currentInterObjScript.endSentence + 1)
                {
                    dialoguePanel.SetActive(false);
                    currentInterObjScript.currentSentence = currentSentenceOriginal;
                }
            }


        }


        if(Input.GetKeyDown(KeyCode.Return))
        {
            
            if (playerScript.isInChat)
            {
                
                playerScript.isInChat = false;
                chatMessages.AddLast(inputField.text);
                updateChat();
                inputField.DeactivateInputField();
                inputField.text = "";
                
            }
            else
            {
                // record keypress into inputfield text
                inputField.ActivateInputField();
                playerScript.isInChat = true;
            }
            
        }
        
    }

    private void updateChat()
    {
        chatContent.text = "";
        foreach(string line in chatMessages)
        {
            chatContent.text += line + "\n";
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject"))
        {
            currentInterObject = collision.gameObject;
            currentInterObjScript = currentInterObject.GetComponent<InteractionObject>();
            currentSentenceOriginal = currentInterObjScript.currentSentence;
            if (currentInterObjScript.textFile)
            {
            currentInterObjScript.sentences = currentInterObjScript.textFile.text.Split('\n');
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject") && 
            collision.gameObject == currentInterObject)
        {
            currentInterObjScript.sentences = null;
            currentInterObject = null;
        }

        dialoguePanel.SetActive(false);  //close the dialogue panel it case it opened

        
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
