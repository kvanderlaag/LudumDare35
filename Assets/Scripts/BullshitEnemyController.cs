using UnityEngine;
using System.Collections;

public class BullshitEnemyController : MonoBehaviour {

    public Transform enemyObject;

    private NavMeshAgent navMeshAgent;
    private bool bFoundPath = false;

    public enum EPath
    {
        TOP,
        BOTTOM
    }

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

   	// Use this for initialization
	public void SetPath (EPath path) {
        if (path == EPath.TOP)
        {
            navMeshAgent.destination = GameObject.Find("TopPath").transform.position;
        } else if (path == EPath.BOTTOM)
        {
            navMeshAgent.destination = GameObject.Find("BottomPath").transform.position;
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!bFoundPath)
        {
            Vector3 target = new Vector3(navMeshAgent.destination.x, 0f, navMeshAgent.destination.z);
            Vector3 actual = new Vector3(transform.position.x, 0f, transform.position.z);
            if (target == actual)
            {
                navMeshAgent.destination = GameObject.Find("Gate(Clone)").transform.position;
                bFoundPath = true;
            }

        }
        //transform.LookAt(playerHead);
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, Mathf.Sin(Time.time) * 5f);
	}
}
