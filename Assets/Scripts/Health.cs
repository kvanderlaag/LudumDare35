using UnityEngine;
using System.Collections;

public enum EDamageType
{
	WEREWOLF,
	VAMPIRE,
	ALIEN
}

public struct DamageInfo
{
	public EDamageType damageType;
	public int damageAmount;
	public bool bReveals;
}

public class Health : MonoBehaviour
{
	public int maxHealth { get; private set; }
	public int curHealth { get; private set; }

	public EDamageType monsterType;
	public bool bIsRevealed { get; private set; }

	public void TakeDamage(DamageInfo damageInfo)
	{
		//reveal monster if damage type is correct
		if (!bIsRevealed && damageInfo.damageType == monsterType && damageInfo.bReveals)
		{
			RevealMonster();
		}

		if (damageInfo.damageType == monsterType)
		{
			curHealth -= damageInfo.damageAmount;
			if (curHealth < 0)
			{
				MonsterDies();
			}
		}
		else
		{
			curHealth -= damageInfo.damageAmount / 4;
			if (curHealth < 0)
			{
				MonsterDies();
			}
		}
	}

	private void RevealMonster()
	{
		bIsRevealed = true;
		//do revealing stuff
	}

	public void MonsterDies()
	{
		curHealth = 0;
		//monster dying things happen
	}
}
