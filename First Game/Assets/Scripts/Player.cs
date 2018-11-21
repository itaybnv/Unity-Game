using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables
    public float speed;
    public Animator animator;

    public float Health;
    public Text healthText;

    public bool isInChat;

    

    //Methods
    void Start()
    {
        healthText.text = "Health: " + Mathf.Round(Health);
        isInChat = false;
    }
    void Update ()
    {
        //Health Text
        healthText.text = "Health: " + Mathf.Round(Health);

        // Player movement
        if (!isInChat)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                animator.SetBool("Moving Right", true);
            }
            else animator.SetBool("Moving Right", false);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                animator.SetBool("Moving Up", true);
            }
            else animator.SetBool("Moving Up", false);
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                animator.SetBool("Moving Left", true);
            }
            else animator.SetBool("Moving Left", false);
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                animator.SetBool("Moving Down", true);
            }
            else animator.SetBool("Moving Down", false);
            
        }
    }
}
