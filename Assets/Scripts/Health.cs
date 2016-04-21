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

    public RevealEnemy reveal;

    public int maxHealth;
	public int curHealth { get; private set; }

	public EDamageType monsterType;
	public bool bIsRevealed { get; private set; }

    public GameState gameState;

    public Renderer frontRenderer, backRenderer;
    public float fadeOutDuration = 0.25f;
    private float fadeOutElapsed = 0f;

    private EnemySounds sounds;

    public void Awake()
    {
        sounds = GetComponent<EnemySounds>();
        reveal = GetComponent<RevealEnemy>();
        curHealth = maxHealth;
        gameState = GameObject.Find("Map").GetComponent<GameState>();
    }

	public void TakeDamage(DamageInfo damageInfo)
	{
		//reveal monster if damage type is correct
		if (!bIsRevealed && damageInfo.damageType == monsterType && damageInfo.bReveals)
		{
            //Debug.Log("Revealing monster " + gameObject.name + " of type " + monsterType);
			RevealMonster(damageInfo);
		}

		if (damageInfo.damageType == monsterType)
		{
			curHealth -= damageInfo.damageAmount;
            //Debug.Log(gameObject.name + " took " + damageInfo.damageAmount + " damage.");
            if (curHealth <= 0)
            {
                MonsterDies();
            } else
            {
                sounds.Hit();
            }
            
		}
		else
		{
			curHealth -= damageInfo.damageAmount / 4;
            //Debug.Log(gameObject.name + " took " + (damageInfo.damageAmount / 4) + " damage.");
            if (curHealth <= 0)
			{
				MonsterDies();
			} else
            {
                sounds.Hit();
            }
		}
	}

	private void RevealMonster(DamageInfo damageInfo)
	{
		bIsRevealed = true;
        //do revealing stuff
        reveal.Reveal(damageInfo.damageType);
        
	}

	public void MonsterDies()
	{
		curHealth = 0;
<<<<<<< HEAD
=======
        gameState.waveEnemies[gameState.currentWave]--;
>>>>>>> origin/master
        //monster dying things happen
        sounds.Die();
        StartCoroutine(FadeOut());
        //Destroy(gameObject);
	}

    IEnumerator FadeOut()
    {
        while (fadeOutElapsed < fadeOutDuration)
        {
            fadeOutElapsed = Mathf.Min(fadeOutDuration, fadeOutElapsed + Time.deltaTime);
            frontRenderer.material.color = new Color(frontRenderer.material.color.r, frontRenderer.material.color.b, frontRenderer.material.color.g, Mathf.Lerp(1, 0, fadeOutElapsed / fadeOutDuration));
            backRenderer.material.color = new Color(backRenderer.material.color.r, backRenderer.material.color.b, backRenderer.material.color.g, Mathf.Lerp(1, 0, fadeOutElapsed / fadeOutDuration));
            yield return null;
        }
<<<<<<< HEAD
        gameState.waveEnemies[gameState.currentWave]--;
=======
>>>>>>> origin/master
        Destroy(gameObject);
    }
}
