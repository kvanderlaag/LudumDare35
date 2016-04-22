using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour {

	public bool ignoreViveReq;

	// Use this for initialization
	void Awake () {
	    if (SteamVR.instance == null && !ignoreViveReq)
        {
            SceneManager.LoadScene("Error");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
