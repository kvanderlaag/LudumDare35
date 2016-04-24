using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

    public float duration = 5f;
    private float elapsed = 0f;

	// Use this for initialization
	void Awake () {
        elapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed >= duration)
            Destroy(gameObject);
	}
}
