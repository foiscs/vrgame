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

            if (!GameObject.Find("AudioPeer").GetComponent<AudioSource>().isPlaying)
            {
                GameManager.Instance.LoadMusicInPlayScene();
                GameObject.Find("AudioPeer").GetComponent<AudioPeer>()._audioSource.Play();
            }
        }
        
    }

    private void ActivateSound()
    {
        source.Play();
    }
}
