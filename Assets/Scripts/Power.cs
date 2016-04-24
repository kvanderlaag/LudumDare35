using UnityEngine;

public class Power : MonoBehaviour
{
	public DamageInfo damage;
	public float aoeRadius;
	public bool bIsWeak;

    public Transform particleEffect;

	public float cooldown;
	public float cooldownTimerCur { get; protected set; }

	// returns true if casted, returns false if cannot be casted
	virtual public bool Execute(Vector3 location)
	{
		if (cooldownTimerCur > 0f) return false; // fails to cast

        cooldownTimerCur = cooldown;

		Collider[] aoeCollisions = Physics.OverlapSphere(location, aoeRadius);
        SpawnParticles(location);

		foreach (Collider c in aoeCollisions)
		{
			if (c.gameObject.CompareTag("Enemy"))
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

    public void SpawnParticles(Vector3 location)
    {
        Instantiate(particleEffect, location, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        
    }
}
