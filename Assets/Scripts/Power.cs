using UnityEngine;

public class Power : MonoBehaviour
{
	public DamageInfo damage;
	public float aoeRadius;
	public bool bIsWeak;

	public float cooldown;
	protected float cooldownTimerCur;

	// returns true if casted, returns false if cannot be casted
	virtual public bool Execute(Vector3 location)
	{
		if (cooldownTimerCur > 0f) return false; // fails to cast

		Collider[] aoeCollisions = Physics.OverlapSphere(location, aoeRadius);

		foreach (Collider c in aoeCollisions)
		{
			if (c.gameObject.tag == "Enemy")
			{
				c.gameObject.GetComponent<Health>().TakeDamage(damage);
			}
		}

		return true;
	}

	virtual public void Update()
	{
		if (cooldownTimerCur > 0f)
		{
			cooldownTimerCur -= Time.deltaTime;
		}

		if (cooldownTimerCur < 0f)
		{
			cooldownTimerCur = 0f;
			
		}
	}

	public bool IsReady()
	{
		return (cooldownTimerCur > 0f)? false : true;
	}
}
