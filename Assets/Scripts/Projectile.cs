using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float projectileSpeed;
	public DamageInfo damage;
	public GameObject target;


	public void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
            return;
		}

		transform.LookAt(target.transform.position);

		transform.position += transform.forward * projectileSpeed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == target)
		{
			target.GetComponent<Health>().TakeDamage(damage);
            //Debug.Log("Doing " + damage.damageAmount + " damage to " + other.gameObject.name);
            Destroy(gameObject);
        }		    
	}
}
