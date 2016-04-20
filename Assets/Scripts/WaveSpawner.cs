using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

[Serializable]
public class Mob
{
	/* 0 = random
	 * 1 = werewolf
	 * 2 = vampire
	 * 3 = alien
	 */
	public int time;
	public int type;// 0 to 3
	public int count;
	public bool bHasSpawned;
}

public class WaveInfo
{
	public Mob[] mobs;
    public int enemyCount;
}

public class WaveSpawner : MonoBehaviour
{
	public GameState gameState;
	public TextAsset wavesJson;
	WaveInfo[] waves;

    public Transform enemyObject;
    public Transform enemySpawn;
    public float mobEnemyDelay;

	public bool bAllMobsSpawned { get; private set; }

	public void Awake()
	{
		// read json file and construct data here
		string waveFileStr = wavesJson.text;
		string[] wavesStrs = JsonHelper.GetJsonObjects(waveFileStr, "Wave");

		List<WaveInfo> tempWaves = new List<WaveInfo>();

		foreach (string w in wavesStrs)
		{
			WaveInfo wave = new WaveInfo();
			string[] mobStrs = JsonHelper.GetJsonObjects(w, "Mob");

			List<Mob> tempMobs = new List<Mob>();

			foreach (string m in mobStrs)
			{
				// fill in the mob object values
				//Debug.Log(m + "\n");
				Mob mob = JsonUtility.FromJson<Mob>(m);
				mob.bHasSpawned = false;

				// add the mob object to the wave
				tempMobs.Add(mob);
                wave.enemyCount += mob.count;
			}
			
			wave.mobs = tempMobs.ToArray();
			tempWaves.Add(wave);
		}

		waves = tempWaves.ToArray();
        gameState.waveEnemies = new int[tempWaves.Count + 1];
        gameState.waveEnemies[0] = 0;

        int i = 1;
        foreach (WaveInfo w in waves)
        {
            gameState.waveEnemies[i] = w.enemyCount;
            i++;
        }
        //Debug.Log(waves.Length);
	}

	public void Update()
	{
		if (gameState.currentWave == 0) return;
  
		foreach (Mob m in waves[gameState.currentWave-1].mobs)
		{
			if (!m.bHasSpawned && gameState.currentWaveTime > m.time)
			{
                //spawn the mob
                Transform tempEnemyMob = Instantiate(enemyObject, enemySpawn.position, enemySpawn.rotation) as Transform;
                if (tempEnemyMob)
                {
                    EDamageType mType;
                    if (m.type == 0) {
                        mType = (EDamageType) Mathf.Ceil(UnityEngine.Random.Range(0.0f, 2.0f));
                    } else if (m.type == 1)
                    {
                        mType = EDamageType.WEREWOLF;
                    } else if (m.type == 2)
                    {
                        mType = EDamageType.VAMPIRE;
                    } else
                    {
                        mType = EDamageType.ALIEN;
                    }

                    int path = (int) Mathf.Ceil(UnityEngine.Random.Range(0f, 2f));
                    if (path == 1)
                    {
                        tempEnemyMob.GetComponent<BullshitEnemyController>().SetPath(BullshitEnemyController.EPath.TOP);
                    } else if (path == 2)
                    {
                        tempEnemyMob.GetComponent<BullshitEnemyController>().SetPath(BullshitEnemyController.EPath.BOTTOM);
                    }


                    tempEnemyMob.GetComponent<EnemyMobSpawn>().mobType = mType;
                    tempEnemyMob.GetComponent<EnemyMobSpawn>().SpawnMobs(m.count);
                }
                //StartCoroutine(SpawnMob(m));

				m.bHasSpawned = true;
			}
		}
	}

    IEnumerator SpawnMob(Mob m)
    {
        
        yield return null;        
    }
}
