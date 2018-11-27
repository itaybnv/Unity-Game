using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour 
{
	//Variables
		//Public Variables
	public Image healthBar;
	public Image healthBarRed;
	public Transform[] patrolPoints;
	public float movementSpeed;
	public int maxHealth = 100;
	public float chaseRadius;
	public bool ranged;
	public bool melee;

		//Private Variables
	private int i = 0;
	private Transform nextPatrolPoint;
	private int health;
	private int redHealth;
	private int maxRedHealth;
	private GameObject chaseGameObject;



	//Methods
	void Start()
	{
		nextPatrolPoint = patrolPoints[i];
		health = maxHealth;
		redHealth = maxHealth;
		maxRedHealth = redHealth;
	}
	void Update()
	{
		if (!chaseGameObject)
		{
			Patrol();
			lookForChaseObject();
		}
		else chase();
	}

	void Patrol()
	{
		// if reached the patrol point
		if (Vector3.Distance(transform.position, patrolPoints[i].transform.position) < 0.5)
		{
			i++;
			// reset i back to start when reached last patrol point
			if (i >= patrolPoints.Length)
			{
				i = 0;
			}
		}
		transform.position = Vector2.MoveTowards(transform.position, patrolPoints[i].transform.position, movementSpeed * Time.deltaTime);
	}

	private void lookForChaseObject()
	{
		Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, chaseRadius);
		for (int i = 0; i < hitObjects.Length; i++)
		{
			if(hitObjects[i].CompareTag("Player"))
			{
				chaseGameObject = hitObjects[i].gameObject;
				break;
			}	
		}
	}
	void chase()
	{
		if(Vector2.Distance(transform.position, chaseGameObject.transform.position) > 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, chaseGameObject.transform.position, movementSpeed * Time.deltaTime);
		}
		else chaseGameObject = null;
	}
	
	public void TakeDamage(int damage)
    {
        //health -= (damage) / (armorValue * armorEffectiveness);  // TODO: Fine tune the damage module to work nicely
		StartCoroutine(ReduceRedHealth(damage));
        health -= damage;
		healthBar.fillAmount = (float)health / (float)maxHealth;
        if (health <= 0)
        {
            health = 0; // in case animation of lowering hp playes don't want -amount of hp
            GameObject.Destroy(gameObject);

        }
    }

	IEnumerator ReduceRedHealth(int damage)
    {
		yield return new WaitForSeconds(0.5f);
		while (damage >= 0)
		{
			redHealth -= 1;
			healthBarRed.fillAmount = (float)redHealth / (float)maxRedHealth;
			damage--;
			yield return new WaitForSeconds(0.01f);
		}
    }
}
