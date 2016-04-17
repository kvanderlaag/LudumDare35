using UnityEngine;
using System.Collections;

public enum ETowerState
{
	WEREWOLF,
	VAMPIRE,
	ALIEN
}

public class Tower : MonoBehaviour
{
	public ETowerState towerState;
	public float towerSwitchTime;
	public float towerSwitchTimerCur { get; private set; }
	public bool bReady { get; private set; }

	public DamageInfo damage;
	public float towerRadius;

	public Projectile werewolfShot, vampireShot, alienshot;

	public void SwitchTower(ETowerState newState)
	{
		if (newState == towerState) return;

		bReady = false;
		towerSwitchTimerCur = towerSwitchTime;
		towerState = newState;
		
		// a bit redundant, but we may want damage type seperate from tower state
		switch (newState)
		{
		case ETowerState.WEREWOLF:
			damage.damageType = EDamageType.WEREWOLF;
			break;
		case ETowerState.VAMPIRE:
			damage.damageType = EDamageType.VAMPIRE;
			break;
		case ETowerState.ALIEN:
			damage.damageType = EDamageType.ALIEN;
			break;
		default:
			break;
		}
	}

	public void FireUpdate()
	{
		// check if any enemy in radius, attack nearest if so
		Vector3 halfCapHeight = Vector3.up * 20f; // big so as to ignore 3rd dimension
		RaycastHit[] enemiesInRange = Physics.CapsuleCastAll(transform.position + halfCapHeight, transform.position - halfCapHeight, towerRadius, Vector3.up);

		foreach (RaycastHit enemyHit in enemiesInRange)
		{
			//check if hit is an enemy, then
			//logic for picking target
			//fire if able
		}
	}

	public void Update()
	{
		if (towerSwitchTimerCur > 0f)
		{
			towerSwitchTimerCur -= Time.deltaTime;
		}

		if (towerSwitchTimerCur < 0f)
		{
			bReady = true;
		}

		FireUpdate();
	}
}
