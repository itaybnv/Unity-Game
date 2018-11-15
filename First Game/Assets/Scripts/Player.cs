using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    public float speed;
    public Animator animator;

    //Methods
    void Update ()
    {
        // Player movement
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
