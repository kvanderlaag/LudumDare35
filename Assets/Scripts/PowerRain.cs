using UnityEngine;
using System.Collections;

public class PowerRain : Power
{

	public void Awake()
	{
		damage.damageType = EDamageType.ALIEN;
		damage.damageAmount = 4;
		damage.bReveals = true;
	}

}
