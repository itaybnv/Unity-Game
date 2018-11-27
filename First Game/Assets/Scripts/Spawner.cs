using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	//Variables
	public Vector2 topLeft;
	public Vector2 bottomRight;
	public float spawnInterval;
	public float startingSpawnCount;
	public GameObject spawnObject;
	public bool spawnEveryClear;
	public bool constantSpawn;

	void Start()
	{
		for (int i = 0; i < startingSpawnCount; i++)
		{
			Instantiate(spawnObject,new Vector2(Random.Range(topLeft.x, bottomRight.x) + transform.position.x ,Random.Range(topLeft.y, bottomRight.y) + transform.position.y),Quaternion.identity);
			Debug.Log("spawn");
		}
	}

}
