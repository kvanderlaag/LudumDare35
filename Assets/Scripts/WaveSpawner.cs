using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

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
	public TextAsset wavesJson;
	WaveInfo[] waves;

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
	}

	// starts at wave number 1
	public void SpawnWave(int waveNum)
	{
		// spawn waves[waveNum-1]
	}	
}
