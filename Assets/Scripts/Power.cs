using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
	public DamageInfo damage;
	public float aoeRadius;
	public bool bIsWeak;

	public float cooldown;
	private float cooldownTimerCur;

	virtual public void Execute(Vector3 location)
	{
		Collider[] aoeCollisions = Physics.OverlapSphere(location, aoeRadius);

		foreach (Collider c in aoeCollisions)
		{
			//apply damage using TakeDamage(damage) to all objects with healthcomp
		}
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
