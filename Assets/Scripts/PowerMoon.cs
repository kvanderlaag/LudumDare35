using UnityEngine;
using System.Collections;

public class PowerMoon : Power 
{

	public void Awake()
	{
		damage.damageType = EDamageType.WEREWOLF;
		damage.damageAmount = 4;
		damage.bReveals = true;
	}

}
