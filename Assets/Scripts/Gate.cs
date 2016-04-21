using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    public int maxHealth;
    public int curHealth { get; private set; }

    public Material gateMat, gateMat2;
    private Color gateColor, gateColor2;

    public GameState gameState;

    public float flashDur = 0.25f;

    public Renderer[] healthCubes;

    public void Awake()
    {
        curHealth = maxHealth;
        gameState = GameObject.Find("Map").GetComponent<GameState>();
        gateMat = GetComponent<Renderer>().materials[0];
        gateMat2 = GetComponent<Renderer>().materials[1];

        gateColor = gateMat.GetColor("_Color");
        gateColor2 = gateMat2.GetColor("_Color");

        foreach (Renderer r in healthCubes)
        {
            r.enabled = true;
        }
    }

	public void TakeDamage()
    {
        curHealth--;

        if (curHealth <= maxHealth / 5 * 4)
        {
            healthCubes[4].enabled = false;
        }

        if (curHealth <= maxHealth / 5 * 3)
        {
            healthCubes[3].enabled = false;
        }

        if (curHealth <= maxHealth / 5 * 2)
        {
            healthCubes[2].enabled = false;
        }

        if (curHealth <= maxHealth / 5 * 1)
        {
            healthCubes[1].enabled = false;
        }

        if (curHealth <= 0)
        {
            healthCubes[0].enabled = false;
        }


        if (curHealth == 0)
        {
            gameState.GameOver();
            Destroy(gameObject);
        }
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        gateMat.SetColor("_Color", Color.red);
        gateMat2.SetColor("_Color", Color.red);
        float elapsed = 0f;
        while (elapsed < flashDur)
        {
            elapsed += Time.deltaTime;
            Color lerpColor = Color.Lerp(Color.red, gateColor, elapsed / flashDur);
            gateMat.SetColor("_Color", lerpColor);
            lerpColor = Color.Lerp(Color.red, gateColor2, elapsed / flashDur);
            gateMat2.SetColor("_Color", lerpColor);
            yield return null;
        }
    }
}
