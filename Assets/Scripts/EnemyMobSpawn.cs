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

		GameObject tempEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.LookRotation(transform.forward, transform.up)) as GameObject;
        tempEnemy.transform.parent = transform;
        
		enemiesSpawned++;

		GameObject tempTransform = Instantiate(new GameObject()); //used to spawn enemies
		bool offset = false;
		int curRing = 1;
		while (enemiesSpawned < enemyCount)
		{
			tempTransform.transform.position = transform.position + new Vector3(0,0,-0.2f * transform.localScale.z) + new Vector3(0f,0f,-0.4f * transform.localScale.z) * curRing;
            //tempTransform.transform.position = new Vector3(tempTransform.transform.position.x * transform.localScale.x, tempTransform.transform.position.y, tempTransform.transform.position.z * transform.localScale.z);
			
			if (offset)
			{
				tempTransform.transform.RotateAround(transform.position, transform.up, 30);
			}

			for (int i = 0; i < 6; i++)
			{
				tempEnemy = Instantiate(enemyPrefab, tempTransform.transform.position, Quaternion.LookRotation(transform.forward, transform.up)) as GameObject;
                tempEnemy.transform.parent = transform;

				tempTransform.transform.RotateAround(transform.position, transform.up, 60);

				enemiesSpawned++;
				if (enemiesSpawned >= enemyCount)
					break;
			}
			offset = (offset)? false : true;
			curRing++;
		}
        Destroy(tempTransform);
	}
}
