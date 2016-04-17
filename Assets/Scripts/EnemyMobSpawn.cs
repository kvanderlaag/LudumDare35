using UnityEngine;
using System.Collections;

public class EnemyMobSpawn : MonoBehaviour {

	public GameObject enemyPrefab;

	public void SpawnMobs(int enemyCount)
	{
		if (enemyCount < 1)
		{
			Debug.Log("Spawning empty mob!");
			return;
		}

		int enemiesSpawned = 0;

		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		enemiesSpawned++;

		GameObject tempTransform = Instantiate(new GameObject()); //used to spawn enemies
		bool offset = false;
		int curRing = 1;
		while (enemiesSpawned < enemyCount)
		{
			tempTransform.transform.position = transform.position + new Vector3(0,0,-0.2f) + new Vector3(0f,0f,-0.4f) * curRing;
			
			if (offset)
			{
				tempTransform.transform.RotateAround(transform.position, transform.up, 30);
			}

			for (int i = 0; i < 6; i++)
			{
				Instantiate(enemyPrefab, tempTransform.transform.position, Quaternion.identity);

				tempTransform.transform.RotateAround(transform.position, transform.up, 60);

				enemiesSpawned++;
				if (enemiesSpawned >= enemyCount)
					break;
			}
			offset = (offset)? false : true;
			curRing++;
		}
	}
}
