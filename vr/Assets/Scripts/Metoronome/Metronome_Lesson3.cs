﻿using UnityEngine;
using System;

public class Metronome_Lesson3 : MonoBehaviour {
    /// <summary>
    /// Subscribe to this to listen for ticks coming from the metronome.
    /// This also passes the time that the tick should occur, relative to AudioSettings.dspTime.
    /// That means you can schedule audio system calls in the future with this info.
    /// </summary>
    public event Action<double> Ticked;

    [SerializeField, Tooltip("The tempo in beats per minute"), Range(15f, 200f)] private double _tempo = 120.0;
    [SerializeField, Tooltip("The number of ticks per beat"), Range(1, 8)] private int _subdivisions = 4;
    [Range(4,32)]
    public int beat = 4;
    public float bpm;
    public bool isPlay;
    public bool isPlayOnce;
    // the length of a single tick in seconds
    private double _tickLength;

    // the next tick time, relative to AudioSettings.dspTime
    private double _nextTickTime;

    /// <summary>
    /// Recalculate the tick length and reset the next tick time
    /// </summary>
    private void Reset()
    {
        Recalculate();
        // bump the next tick time ahead the length of one tick so we don't get a double trigger
        _nextTickTime = AudioSettings.dspTime + _tickLength;
    }

    /// <summary>
    /// Derive the length of a tick in seconds from the tempo and subdivisions provided
    /// </summary>
    private void Recalculate()
    {
        double beatsPerSecond = bpm / 60.0;
        double ticksPerSecond = beatsPerSecond * _subdivisions;
        _tickLength = 1.0 / ticksPerSecond;
    }

    /// <summary>
    /// This gets called when the GameObject first gets set up.
    /// Do initialization here.
    /// </summary>
    private void Awake()
    {
        Reset();
    }

    /// <summary>
    /// This gets called in the editor when an inspector control changes.
    /// Recalculate the tick length here.
    /// </summary>
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Recalculate();
        }
    }

    /// <summary>
    /// This gets called once per game frame.
    /// Check to see if we should schedule any ticks here.
    /// </summary>
    private void Update()
    {
        if (isPlay)
        {
            if(!isPlayOnce)
            {
                Debug.Log((4 / beat) / (bpm / 60));
                Reset();
                Recalculate();
                isPlayOnce = true;
            }

            double currentTime = AudioSettings.dspTime;

            // look ahead the length of one frame (approximately), because we'll be scheduling samples
            currentTime += Time.deltaTime;

            // there may be more than one tick within the next frame, so this will catch them all
            while (currentTime > _nextTickTime)
            {
                // if someone has subscribed to ticks from the metronome, let them know we got a tick
                if (Ticked != null)
                {
                    Ticked(_nextTickTime);
                }

                // increment the next tick time
                _nextTickTime += _tickLength;
            }
        }
    }
}
