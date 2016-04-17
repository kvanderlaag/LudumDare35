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
		}

		transform.LookAt(target.transform.position);

		transform.position += transform.forward * projectileSpeed * Time.deltaTime;
	}

	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == target)
		{
			target.GetComponent<Health>().TakeDamage(damage);
		}

		Destroy(gameObject);
	}
}
