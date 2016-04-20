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

    public int maxTowers { get; private set; }
    public int curTowers;

	public int numberOfWaves;
	public int currentWave { get; private set; }
	public float currentWaveTime { get; private set; }

	public EWinState winState;
	public EPhaseState phaseState;

	public float upkeepDuration;
	public float upkeepTimerCur { get; private set; }

	public WaveSpawner waveSpawner;

    public int[] waveEnemies;

    public Renderer instructionsRenderer;
    public Material InstructionsMaterial, WinMaterial, LoseMaterial, AttackMaterial, UpkeepMaterial;

    public void Awake()
    {
        phaseState = EPhaseState.NONE;
        winState = EWinState.PLAYING;

    }

    public void StartGame()
    {
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        EnemyMobSpawn[] enemies = GameObject.FindObjectsOfType<EnemyMobSpawn>();
        Gate[] gate = GameObject.FindObjectsOfType<Gate>();

        foreach (Tower t in towers)
        {
            Destroy(t.gameObject);
        }

        foreach (EnemyMobSpawn e in enemies)
        {
            Destroy(e.gameObject);
        }

        foreach (Gate g in gate)
        {
            Destroy(g.gameObject);
        }

        GetComponent<GateSpawner>().SpawnGate();

        curTowers = 0;
        maxTowers = 5;
        currentWave = 0;
        
        currentWaveTime = 0f;
        EnterUpkeep();
    }

	public void GameOver()
	{
        phaseState = EPhaseState.NONE;
		winState = EWinState.LOST;
        instructionsRenderer.material = LoseMaterial;
		// gameover things happen here
	}

	public void WonGame()
	{
		winState = EWinState.WON;
        phaseState = EPhaseState.NONE;
        instructionsRenderer.material = WinMaterial;
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
        currentWaveTime = 0;
        phaseState = EPhaseState.ENEMYWAVE;
        instructionsRenderer.material = AttackMaterial;
        // wave starting things happen here
    }

	public void EnterUpkeep()
	{
		phaseState = EPhaseState.UPKEEP;
		upkeepTimerCur = upkeepDuration;
        instructionsRenderer.material = UpkeepMaterial;
        // start upkeep here
    }

	public void Update()
	{
		if (phaseState == EPhaseState.UPKEEP)
		{
			upkeepTimerCur -= Time.deltaTime;
            if (upkeepTimerCur < 0f)
            {
                WaveStart();
            }
        } else if (phaseState == EPhaseState.ENEMYWAVE)
        {
            currentWaveTime += Time.deltaTime;
            if (waveEnemies[currentWave] == 0)
                WaveEnd();
        }
		
		
	}
}
