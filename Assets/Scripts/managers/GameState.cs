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


    public DigitController[] timerDigits;

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

        HideTimer();
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

        GetComponent<GameSounds>().StartingSound();
        EnterUpkeep();
    }

	public void GameOver()
	{
        HideTimer();
        GetComponent<GameSounds>().Lose();

        phaseState = EPhaseState.NONE;
		winState = EWinState.LOST;
        instructionsRenderer.material = LoseMaterial;
		// gameover things happen here
	}

	public void WonGame()
	{
        HideTimer();
        GetComponent<GameSounds>().Win();
        winState = EWinState.WON;
        phaseState = EPhaseState.NONE;
        instructionsRenderer.material = WinMaterial;
        // winning things happen here
    }

	public void WaveEnd()
	{
		phaseState = EPhaseState.UPKEEP;

        HideTimer();

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
        ShowTimer();
		currentWave++;
        currentWaveTime = 0;
        phaseState = EPhaseState.ENEMYWAVE;
        instructionsRenderer.material = AttackMaterial;
        // wave starting things happen here
    }

	public void EnterUpkeep()
	{
        ShowTimer();
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
            UpkeepTimer();
            if (upkeepTimerCur < 0f)
            { 
                WaveStart();
            }
        } else if (phaseState == EPhaseState.ENEMYWAVE)
        {
            currentWaveTime += Time.deltaTime;
            WaveTimer();
            if (waveEnemies[currentWave] <= 0)
                WaveEnd();
        }
		
		
	}

    private void UpkeepTimer()
    {
        if (upkeepTimerCur > 0)
        {
            timerDigits[0].Change((int)upkeepTimerCur % 10);
            timerDigits[0].Show();
        }
        else
        {
            timerDigits[0].Hide();
        }

        if (upkeepTimerCur > 10)
        {
            timerDigits[1].Change((int)upkeepTimerCur % 100 / 10);
            timerDigits[1].Show();
        }
        else
        {
            timerDigits[1].Hide();
        }

        if (upkeepTimerCur > 100)
        {
            timerDigits[2].Change((int)upkeepTimerCur % 1000 / 100);
            timerDigits[2].Show();
        }
        else
        {
            timerDigits[2].Hide();
        }

    }

    private void WaveTimer()
    {
        if (currentWaveTime > 0)
        {
            timerDigits[0].Change((int) currentWaveTime % 10);
            timerDigits[0].Show();
        } else
        {
            timerDigits[0].Hide();
        }
        

        if (currentWaveTime > 10)
        {
            timerDigits[1].Change((int) currentWaveTime % 100 / 10);
            timerDigits[1].Show();
        }
        else
        {
            timerDigits[1].Hide();
        }

        if (currentWaveTime > 100)
        {
            timerDigits[2].Change((int) currentWaveTime % 1000 / 100);
            timerDigits[2].Show();
        }
        else
        {
            timerDigits[2].Hide();
        }
    }

    private void HideTimer()
    {
        timerDigits[0].Hide();
        timerDigits[1].Hide();
        timerDigits[2].Hide();
    }

    private void ShowTimer()
    {
        timerDigits[0].Show();
        timerDigits[1].Show();
        timerDigits[2].Show();
    }
}
