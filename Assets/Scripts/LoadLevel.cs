using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public static int levelCount = 3;

	// Use this for initialization
	public static void Load () {

        Debug.Log("Levels: " + levelCount);
        int levelToLoad = Random.Range(1, levelCount + 1);
        Debug.Log("Load level: " + levelToLoad);
        SteamVR_LoadLevel.Begin("Level" + levelToLoad, false, 0.5f, 0, 0, 0, 1);
	}
}
