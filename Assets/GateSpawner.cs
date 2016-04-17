using UnityEngine;
using System.Collections;

public class GateSpawner : MonoBehaviour {

    public Transform gateObject;
    public Transform gateSpawn;

	// Use this for initialization
	void Start () {
        Instantiate(gateObject, gateSpawn.position, gateSpawn.rotation);
	}
}
