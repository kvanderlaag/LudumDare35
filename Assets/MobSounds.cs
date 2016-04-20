using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MobSounds : MonoBehaviour {

    private AudioSource source;

    public AudioClip spawnClip;

    // Use this for initialization
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Spawn()
    {
        source.clip = spawnClip;
        source.Play();
    }
}
