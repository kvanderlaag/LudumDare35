using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TowerSounds : MonoBehaviour {

    private AudioSource source;

    public AudioClip attackClip, placeClip;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Attack()
    {
        source.clip = attackClip;
        source.Play();
    }

    public void Place()
    {
        source = GetComponent<AudioSource>();
        source.clip = placeClip;
        source.Play();
    }
}
