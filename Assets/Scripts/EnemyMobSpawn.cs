using UnityEngine;
using System.Collections;

public class EnemyMobSpawn : MonoBehaviour {

	public GameObject enemyPrefab;

    public EDamageType mobType;

    private MobSounds sounds;

    public void Awake()
    {
        sounds = GetComponent<MobSounds>();
    }

	public void SpawnMobs(int enemyCount)
	{
		if (enemyCount < 1)
		{
			Debug.Log("Spawning empty mob!");
			return;
		}

		int enemiesSpawned = 0;

		GameObject tempEnemy = Instantiate(enemyPrefab) as GameObject;
		Vector2 randomJitter = Random.insideUnitCircle * 0.2f;
		tempEnemy.transform.position = transform.position + new Vector3(randomJitter.x, 0f, randomJitter.y) * transform.localScale.z;
		tempEnemy.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        tempEnemy.transform.parent = transform;
        tempEnemy.GetComponent<Health>().monsterType = mobType;
        
		enemiesSpawned++;

		GameObject tempTransform = Instantiate(new GameObject()); //used to spawn enemies
		bool offset = false;
		int curRing = 1;
		while (enemiesSpawned < enemyCount)
		{
			tempTransform.transform.position = transform.position + new Vector3(0,0,-0.2f * transform.localScale.z)
				+ new Vector3(0f,0f,-0.4f * transform.localScale.z) * curRing;
			
			if (offset)
			{
				tempTransform.transform.RotateAround(transform.position, transform.up, 30);
			}

			for (int i = 0; i < 6; i++)
			{
				GameObject tempEnemy2 = Instantiate(enemyPrefab) as GameObject;
				Vector2 randomJitter2 = Random.insideUnitCircle * 0.2f;
				tempEnemy2.transform.position = tempTransform.transform.position + new Vector3(randomJitter2.x, 0f, randomJitter2.y) * transform.localScale.z;
				tempEnemy2.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                tempEnemy2.transform.parent = transform;
                tempEnemy2.GetComponent<Health>().monsterType = mobType;

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

