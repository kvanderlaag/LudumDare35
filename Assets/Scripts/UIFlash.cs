using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class UIFlash : MonoBehaviour {

    public float flashDuration = 0.5f;
    private float timeElapsed;
    private Renderer ren;

	// Use this for initialization
	void OnEnable () {
        timeElapsed = 0f;
        ren = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed  >= flashDuration)
        {
            ren.enabled = !ren.enabled;
            timeElapsed = 0f;
        }
	}
}
