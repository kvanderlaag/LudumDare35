using UnityEngine;
using System.Collections;

public class GateSpawner : MonoBehaviour {

    public Transform gateObject;
    public Transform gateSpawn;

	// Use this for initialization
	public void SpawnGate () {
        Instantiate(gateObject, gateSpawn.position, gateSpawn.rotation);
	}
}
