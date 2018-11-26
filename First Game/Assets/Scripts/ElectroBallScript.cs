using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBallScript : MonoBehaviour 
{
	//Variables
	public float maxRange = 100f;
	private float distanceTravelled = 0;
	private Vector3 lastPosition;
	public int damage = 30;
	//Methods

	void Start()
	{
	lastPosition = transform.position;
	}
	public void Update()
	{
		if (distanceTravelled >= maxRange)
		{
			GameObject.Destroy(gameObject);
		}

		distanceTravelled += Vector3.Distance(transform.position, lastPosition);
  		lastPosition = transform.position;
		isColliding = false;
		
		
	}
	private bool isColliding;
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isColliding) return;
		isColliding = true;
		// if the collision isn't the player
		if (!collision.CompareTag("Player")){
			// if the collision is a destructable interaction object Or an enemy
			if ((collision.CompareTag("interObject") && collision.GetComponent<InteractionObject>().destroyAble) || collision.CompareTag("Enemy"))
			{
				GameObject.Destroy(gameObject);
				collision.gameObject.SendMessage("TakeDamage", damage);
				Debug.Log("Electro Hit " + collision.name + " For " + damage + " Damage ");
				gameObject.GetComponent<CircleCollider2D>().enabled = false;
			}
		}
	}
}
