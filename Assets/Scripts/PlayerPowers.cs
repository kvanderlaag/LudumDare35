using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour
{
	// shared cooldown and timer of weak powers (strong powers have own cooldown
	public float weakPowerSharedCD;
	private float weakPowerTimerCur;

	public Power[] powers;
	public int currentPowerIdx;

    public void Awake()
    {
        DamageInfo d = new DamageInfo();
        d.bReveals = true;
        d.damageAmount = 4;
        
        d.damageType = EDamageType.WEREWOLF;

        powers[0].damage = d;

        d.bReveals = true;
        d.damageAmount = 4;
        d.damageType = EDamageType.VAMPIRE;

        powers[1].damage = d;

        d.bReveals = true;
        d.damageAmount = 4;
        d.damageType = EDamageType.ALIEN;

        powers[2].damage = d;
    }

	public bool AttemptExecutePower(Vector3 executePos)
	{
        if (powers[currentPowerIdx].Execute(executePos))
            return true;
        return false;
	}

	
}
