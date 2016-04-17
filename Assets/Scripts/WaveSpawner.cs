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
	public float type;// 0 to 3
	public int count;
	public bool bHasSpawned;
}

public class WaveInfo
{
	public Mob[] mobs;
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
				Debug.Log(m + "\n");
				Mob mob = JsonUtility.FromJson<Mob>(m);
				mob.bHasSpawned = false;

				// add the mob object to the wave
				tempMobs.Add(mob);
			}
			
			wave.mobs = tempMobs.ToArray();
			tempWaves.Add(wave);
		}

		waves = tempWaves.ToArray();
        Debug.Log(waves.Length);
	}

	public void Update()
	{
		if (gameState.currentWave == 0) return;
  
		foreach (Mob m in waves[gameState.currentWave-1].mobs)
		{
			if (!m.bHasSpawned && gameState.currentWaveTime > m.time)
			{
                //spawn the mob
                StartCoroutine(SpawnMob(m));

				m.bHasSpawned = true;
			}
		}
	}

    IEnumerator SpawnMob(Mob m)
    {
        for (int i = 0; i < m.count; ++i)
        {
            Instantiate(enemyObject, enemySpawn.position, enemySpawn.rotation);
            yield return new WaitForSeconds(mobEnemyDelay);
        }
        
    }
}
