using UnityEngine;
using System.Collections.Generic;

public enum ETowerState
{
	WEREWOLF,
	VAMPIRE,
	ALIEN
}

public class Tower : MonoBehaviour
{
	public ETowerState towerState { get; private set; }
	public GameObject target;

	public float fireRate;
	private float fireCDTimerCur;

	public float towerSwitchTime;
	public float towerSwitchTimerCur { get; private set; }

	public bool bReady { get; private set; }

	public DamageInfo damage;
	public float towerRadius;

	public GameObject werewolfShot, vampireShot, alienShot;

	public void Awake()
	{
		towerState = ETowerState.WEREWOLF;
		bReady = true;
	}

	public void SwitchTower(ETowerState newState)
	{
		bReady = false;
		towerSwitchTimerCur = towerSwitchTime;

		SwitchTowerInstant(newState);
	}

	public void SwitchTowerInstant(ETowerState newState)
	{
		if (newState == towerState) return;

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
		SeekUpdate();
		FireUpdate();
	}

	public void SeekUpdate()
	{
		if (target != null)
		{
			Vector2 flatTargetDist = new Vector2(
					transform.position.x - target.transform.position.x,
					transform.position.z - target.transform.position.z
					);
			
			if (flatTargetDist.magnitude > towerRadius)
				target = null;
		}

		if (target != null) return;

		// check if any enemy in radius, attack nearest if so
		Vector3 halfCapHeight = Vector3.up * 20f; // big so as to ignore 3rd dimension

		//get all enemies in range
		RaycastHit[] enemiesInRange = Physics.CapsuleCastAll(
				transform.position + halfCapHeight, 
				transform.position - halfCapHeight,
				towerRadius, Vector3.up);

		List<GameObject> enemies = new List<GameObject>();
		foreach (RaycastHit hit in enemiesInRange)
		{
			if (hit.collider.gameObject.tag == "Enemy")
			{
				enemies.Add(hit.collider.gameObject);
			}
		}

		//if there's no enemies, exit
		if (enemies.Count < 1) return;

		//if there are enemies, find closest
		GameObject closest = null;
		foreach (GameObject eObj in enemies)
		{
			if (closest == null) closest = eObj;
			else
			{
				if ((eObj.transform.position - transform.position).magnitude >
						(closest.transform.position - transform.position).magnitude)
				{
					closest = eObj;
				}
			}
		}

		target = closest;
	}

	private void FireUpdate()
	{
		if (fireCDTimerCur > 0f)
			fireCDTimerCur -= Time.deltaTime;

		if (fireCDTimerCur <= 0f)
			fireCDTimerCur = 0f;

		if (target == null || !bReady) return;

		if (fireCDTimerCur <= 0f)
		{
			switch(towerState) {
			case ETowerState.WEREWOLF:
				LaunchProjectile(werewolfShot);
				break;
			case ETowerState.VAMPIRE:
				LaunchProjectile(vampireShot);
				break;
			case ETowerState.ALIEN:
				LaunchProjectile(alienShot);
				break;
			default:
				break;
			}
		}

	}

	private void LaunchProjectile(GameObject shot)
	{
		if (target == null)
		{
			Debug.Log("Null target when trying to launch projectile!");
		}

		Instantiate(shot, transform.position, Quaternion.identity);
		Projectile projectileComp = shot.GetComponent<Projectile>();
		projectileComp.damage = damage;
		projectileComp.target = target;

		Debug.Log(gameObject.name + " Shoot!");

		fireCDTimerCur = fireRate;
	}
}
