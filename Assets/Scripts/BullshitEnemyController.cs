﻿using UnityEngine;
using System.Collections;

public class BullshitEnemyController : MonoBehaviour {

    public Transform enemyObject;
	public PathDirector nextTarget;
	public float pathAcceptRadius;

    private NavMeshAgent navMeshAgent;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
		nextTarget = GameObject.Find("00 EnemySpawn").GetComponent<PathDirector>().GetPath();
		navMeshAgent.SetDestination(nextTarget.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (nextTarget != null && (nextTarget.transform.position - transform.position).magnitude < pathAcceptRadius)
        {
			//Debug.Log("New target!");
            nextTarget = nextTarget.GetPath();
			if (nextTarget == null) navMeshAgent.ResetPath();
			else
			{
				navMeshAgent.SetDestination(nextTarget.gameObject.transform.position);
			}
		}
	}
}
