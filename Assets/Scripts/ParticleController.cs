using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Awake () {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ps.IsAlive())
            Destroy(gameObject);
	}
}
