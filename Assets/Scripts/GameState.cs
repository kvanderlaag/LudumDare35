using UnityEngine;
using System.Collections;

public enum EWinState
{
	WON,
	LOST,
	PLAYING
}

public enum EPhaseState
{
	ENEMYWAVE,
	UPKEEP,
	NONE
}

public class GameState : MonoBehaviour
{
	public int numberOfWaves;
	public int currentWave { get; private set; }
	public float currentWaveTime { get; private set; }

	public EWinState winState;
	public EPhaseState phaseState;

	public float upkeepDuration;
	public float upkeepTimerCur { get; private set; }

	public WaveSpawner waveSpawner;

	public void GameOver()
	{
		winState = EWinState.LOST;
		// gameover things happen here
	}

	public void WonGame()
	{
		winState = EWinState.WON;
		// winning things happen here
	}

	public void WaveEnd()
	{
		phaseState = EPhaseState.UPKEEP;
		if (currentWave >= numberOfWaves)
		{
			phaseState = EPhaseState.NONE;
			WonGame();
		}
		else
		{
			EnterUpkeep();
		}
	}

	public void WaveStart()
	{
		currentWave++;
		// wave starting things happen here
	}

	public void EnterUpkeep()
	{
		phaseState = EPhaseState.UPKEEP;
		upkeepTimerCur = upkeepDuration;
		// start upkeep here
	}

	public void Update()
	{
		if (phaseState == EPhaseState.UPKEEP)
		{
			upkeepTimerCur -= Time.deltaTime;
		}

		if (upkeepTimerCur < 0f)
		{
			WaveStart();
		}
		
	}
}
