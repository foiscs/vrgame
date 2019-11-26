using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour 
{

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DrumStickHead")
        {
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            ActivateSound();

            if (!Camera.main.GetComponent<AudioSource>().isPlaying)
                Camera.main.GetComponent<AudioSource>().Play();
        }
    }

    private void ActivateSound()
    {
        source.Play();
    }
}
