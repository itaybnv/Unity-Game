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

    public bool isInChat;

    public Camera camera;
    private float[] cooldowns;
    private float[] cooldownsMax;
    private Image[] cooldownImages;

    

    //Methods
    void Start()
    {
        isInChat = false;
        camera = FindObjectOfType<Camera>();
        cooldowns = new float[4] {playerInfo.ability1CD, playerInfo.ability2CD, playerInfo.ability3CD, playerInfo.ability4CD};
        cooldownsMax = new float[4] {playerInfo.ability1CDMax, playerInfo.ability2CDMax, playerInfo.ability3CDMax, playerInfo.ability4CDMax};
        cooldownImages = new Image[4] {playerInfo.cooldown1Image, playerInfo.cooldown2Image, playerInfo.cooldown3Image, playerInfo.cooldown4Image};
        
    }
    void Update ()
    {
        // Player movement
        if (!isInChat)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                //gameObject.GetComponent<Rigidbody2D>().MovePosition(Vector2.right * speed * Time.deltaTime);
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
            if (Input.GetButtonDown("FirstAbility") && cooldowns[0] <= 0){

                
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
                GameObject newArrow = Instantiate(playerInfo.projectile, transform.position, Quaternion.Euler(0, 0, angle));
                newArrow.AddComponent<ElectroBallScript>();
                newArrow.GetComponent<Rigidbody2D>().AddRelativeForce(playerInfo.projectileSpeed);
                // reset CD
                cooldowns[0] = cooldownsMax[0];
            }
            // check if the second ability was pressed
            if (Input.GetButtonDown("SecondAbility") && cooldowns[1] <= 0)
            {
                Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, playerInfo.attackRange);
                animator.SetTrigger("attacking");
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
                        
                        }
                        // if the object in index I is an enemy
                        if  (hitObjects[i].CompareTag("Enemy"))
                        {
                            hitObjects[i].gameObject.SendMessage("TakeDamage", playerInfo.meleeDamage);
                        }
                        
                    }
                }
                // reset CD
                cooldowns[1] = cooldownsMax[1];
            }
            
        }

        //Handle cooldowns
        for (int i = 0; i < cooldowns.Length; i++)
        {
            Debug.Log("cooldown["+i+"]: "+cooldownsMax[i]);           
            if(cooldowns[i] > 0)
            {
                cooldowns[i] -= 1 * Time.deltaTime;
                

            }
            cooldownImages[i].fillAmount = cooldowns[i] / cooldownsMax[i];
        }
    }
}
