using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
	public DamageInfo damage;
	public float aoeRadius;
	public bool bIsWeak;

	public float cooldown;
	private float cooldownTimerCur;

	virtual public bool Execute(Vector3 location)
	{
		if (cooldownTimerCur > 0f) return false;

		Collider[] aoeCollisions = Physics.OverlapSphere(location, aoeRadius);

		foreach (Collider c in aoeCollisions)
		{
			//apply damage using TakeDamage(damage) to all objects with healthcomp
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
