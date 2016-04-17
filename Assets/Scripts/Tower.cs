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
