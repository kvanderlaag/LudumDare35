using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MenuSounds : MonoBehaviour {

    public AudioClip menuSelectClip;
    private AudioSource source;

	// Use this for initialization
	void OnEnable () {
        source = GetComponent<AudioSource>();
	}
	
	public void MenuSelect()
    {
        source.clip = menuSelectClip;
        source.Play();
    }
}
