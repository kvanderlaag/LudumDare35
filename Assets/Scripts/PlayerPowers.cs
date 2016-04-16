using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour
{
	// shared cooldown and timer of weak powers (strong powers have own cooldown
	public float weakPowerSharedCD;
	private float weakPowerTimerCur;

	public Power[] powers;
	public int currentPowerIdx;

	//how does this work?
	GameObject remoteR;
	GameObject remoteL;

	public void AttemptExecutePower()
	{
		powers[currentPowerIdx].Execute(remoteR.transform.position);
	}

	
}
