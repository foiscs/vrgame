﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour 
{

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DrumStickHead")
        {
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            Debug.Log(other.gameObject.GetComponent<TrackSpeed>().speed);
            ActivateSound();
        }
    }

    private void ActivateSound()
    {
        source.Play();
    }
}