using UnityEngine;
using System.Collections;

public class PowerSun : Power 
{

	public void Awake()
	{
		damage.damageType = EDamageType.VAMPIRE;
		damage.damageAmount = 5;
		damage.bReveals = true;
	}

}
