using UnityEngine;
using System.Collections;
using Valve.VR;

public class LoadLevel : MonoBehaviour {

    public int levels = 2;

	// Use this for initialization
	void Start () {
        int levelToLoad = Random.Range(1, levels + 1);
        SteamVR_LoadLevel.Begin("Level" + levelToLoad, true, 0.5f, 0, 0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
