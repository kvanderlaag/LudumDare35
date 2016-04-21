using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HandSounds : MonoBehaviour {

    public AudioClip spellClip, pickupClip;

    private AudioSource source;

	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void Pickup () {
        source.clip = pickupClip;
        source.Play();
	}

    public void Spell()
    {
        source.clip = spellClip;
        source.Play();
    }
}
