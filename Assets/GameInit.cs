using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	    if (SteamVR.instance == null)
        {
            SceneManager.LoadScene("Error");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
