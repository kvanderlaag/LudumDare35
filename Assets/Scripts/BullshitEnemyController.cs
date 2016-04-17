using UnityEngine;
using System.Collections;

public class BullshitEnemyController : MonoBehaviour {

   	// Use this for initialization
	void Start () {
        GetComponent<NavMeshAgent>().destination = GameObject.Find("GateSpawn").transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(playerHead);
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, Mathf.Sin(Time.time) * 5f);
	}
}
