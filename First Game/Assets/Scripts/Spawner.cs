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
	public float spawnEachTime;
	public bool doSpawn;
	public bool waitForLeftAlive;
	private LinkedList<GameObject> leftAlive;

	void Update()
	{
		var currentNode = leftAlive;
	}

	void Start()
	{
		leftAlive = new LinkedList<GameObject>();
		Spawn(startingSpawnCount);
		StartCoroutine("SpawnRepeat",spawnEachTime);
	}
	void Spawn(float spawnCount)
	{
		for (int i = 0; i < startingSpawnCount; i++)
		{
			GameObject temp = Instantiate(spawnObject,new Vector2(Random.Range(topLeft.x, bottomRight.x) + transform.position.x ,Random.Range(topLeft.y, bottomRight.y) + transform.position.y),Quaternion.identity);
			leftAlive.AddLast(temp);
		}
	}

	IEnumerator SpawnRepeat(float spawnCount)
	{
		float originalSpawnCount = spawnCount;
		yield return new WaitForSeconds(spawnInterval);
		while(doSpawn)
		{
			while(spawnCount > 0)
			{
			GameObject temp = Instantiate(spawnObject,new Vector2(Random.Range(topLeft.x, bottomRight.x) + transform.position.x ,Random.Range(topLeft.y, bottomRight.y) + transform.position.y),Quaternion.identity);
			leftAlive.AddLast(temp);
			spawnCount--;
			}
			spawnCount = originalSpawnCount;
			yield return new WaitForSeconds(spawnInterval);
		}
	}

}
