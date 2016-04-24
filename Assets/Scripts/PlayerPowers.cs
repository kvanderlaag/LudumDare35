using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour
{
	// shared cooldown and timer of weak powers (strong powers have own cooldown
	public float weakPowerSharedCD;
	private float weakPowerTimerCur;

	public Power[] powers;
	public int currentPowerIdx;

    public bool isDay = true;

    private Material sceneSkybox;
    private Light sceneLight;
    public float dayNightShiftTime = 1f;
    public Color dayLightColor, nightLightColor;

    public void Awake()
    {
        sceneSkybox = RenderSettings.skybox;
        sceneLight = GameObject.Find("Directional light").GetComponent<Light>();
        sceneLight.color = dayLightColor;
        sceneSkybox.SetFloat("_Blend", 0f);
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
        {
            if (currentPowerIdx == 0 && isDay)
            {
                StartCoroutine(Night());
            } else if (currentPowerIdx == 1 && !isDay)
            {
                StartCoroutine(Day());
            }

            return true;
            
        }
            
        return false;
	}

    IEnumerator Day()
    {
        float lerpAmount = 0f;
        float startAmount = sceneSkybox.GetFloat("_Blend");
        float timeElapsed = 0f;
        while (timeElapsed < dayNightShiftTime)
        {
            timeElapsed += Time.deltaTime;
            lerpAmount = timeElapsed / dayNightShiftTime;
            sceneSkybox.SetFloat("_Blend", Mathf.Max(0f, startAmount - lerpAmount));
            sceneLight.color = Color.Lerp(nightLightColor, dayLightColor, lerpAmount);
            yield return null;
        }
        isDay = true;
        
    }

    IEnumerator Night()
    {
        float lerpAmount = 0f;
        float startAmount = sceneSkybox.GetFloat("_Blend");
        float timeElapsed = 0f;
        while (timeElapsed < dayNightShiftTime)
        {
            timeElapsed += Time.deltaTime;
            lerpAmount = timeElapsed / dayNightShiftTime;
            sceneSkybox.SetFloat("_Blend", Mathf.Min(1f, lerpAmount + startAmount));
            sceneLight.color = Color.Lerp(dayLightColor, nightLightColor, lerpAmount);
            yield return null;
        }
        isDay = false;
    }

	
}
