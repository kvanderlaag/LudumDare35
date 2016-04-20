using UnityEngine;
using System.Collections;

public class EnemyFadeIn : MonoBehaviour {

    public float fadeDuration = 0.25f;

    private float fadeElapsed = 0f;
    Material material;

	// Use this for initialization
	void Awake () {
        material = GetComponent<Renderer>().material;
        StartCoroutine(FadeIn());
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator FadeIn()
    {
        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed = Mathf.Min(fadeDuration, fadeElapsed + Time.deltaTime);
            material.color = new Color(material.color.r, material.color.b, material.color.g, Mathf.Lerp(0, 1, fadeElapsed / fadeDuration));
            yield return null;
        }
    }
}
