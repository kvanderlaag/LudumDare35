using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {

    public float attackRadius = 0.2f;
    public float attackInterval = 2f;
    private float attackDelay = 0f;

	// Update is called once per frame
	void Update () {
	    if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
            return;
        }

        Vector3 halfCapHeight = Vector3.up * 20f; // big so as to ignore 3rd dimension

        //get all enemies in range
        RaycastHit[] enemiesInRange = Physics.CapsuleCastAll(
                transform.position + halfCapHeight,
                transform.position - halfCapHeight,
                attackRadius, Vector3.up);

        List<GameObject> enemies = new List<GameObject>();
        foreach (RaycastHit hit in enemiesInRange)
        {
            if (hit.collider.gameObject.CompareTag("Gate"))
            {
                hit.collider.GetComponent<Gate>().TakeDamage();
                attackDelay = attackInterval;
            }
        }
    }
}
