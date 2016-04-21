using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EnemySounds : MonoBehaviour {

    private AudioSource source;

    public AudioClip attackClip, hitClip, dieClip;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void Attack () {
        source.clip = attackClip;
        source.Play();
	}

    public void Hit()
    {
        source.clip = hitClip;
        source.Play();
    }

    public void Die()
    {
        source.clip = dieClip;
        source.Play();
    }
}
