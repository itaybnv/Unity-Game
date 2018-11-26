using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables
    public PlayerInfo playerInfo;
    public float speed;
    public Animator animator;

    public float Health;
    public Text healthText;

    public bool isInChat;

    public Camera camera;

    

    //Methods
    void Start()
    {
        healthText.text = "Health: " + Mathf.Round(Health);
        isInChat = false;
        camera = FindObjectOfType<Camera>();
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
            // check if the first ability was pressed
            if (Input.GetButtonDown("FirstAbility")){

                
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
                GameObject newArrow = Instantiate(playerInfo.projectile, transform.position, Quaternion.Euler(0, 0, angle));
                newArrow.AddComponent<ElectroBallScript>();
                newArrow.GetComponent<Rigidbody2D>().AddRelativeForce(playerInfo.projectileSpeed);
            }
            // check if the second ability was pressed
            if (Input.GetButtonDown("SecondAbility"))
            {
                Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, playerInfo.attackRange);
                for (int i = 0; i < hitObjects.Length; i++)
                {
                    Debug.Log("hitObject[" + i + "]: " + hitObjects[i]);
                }
                // loop through all the objects in the melee range
                for (int i = 0; i < hitObjects.Length; i++)
                {
                    // Ignore the player 
                    if (!hitObjects[i].CompareTag("Player"))
                    {
                        // if the object in index I is an interactable object and is destroyable
                        if (hitObjects[i].CompareTag("interObject") && hitObjects[i].GetComponent<InteractionObject>().destroyAble)
                        {
                            hitObjects[i].gameObject.SendMessage("TakeDamage", playerInfo.meleeDamage);
                            break;
                        
                        }
                        // if the object in index I is an enemy
                        if  (hitObjects[i].CompareTag("Enemy"))
                        {
                            hitObjects[i].gameObject.SendMessage("TakeDamage", playerInfo.meleeDamage);
                            break;  //stop damaging on first enemy hit
                        }
                        
                    }
                }
            }
            
        }
    }
}
