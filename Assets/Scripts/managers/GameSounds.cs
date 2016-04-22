using UnityEngine;
using System.Collections;

public class GameSounds : MonoBehaviour {

    public AudioClip startClip, loseClip, winClip;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void StartingSound()
    {
        source.clip = startClip;
        source.Play();
    }

    public void Lose()
    {
        source.clip = loseClip;
        source.Play();
    }

    public void Win()
    {
        source.clip = winClip;
        source.Play();
    }
}
