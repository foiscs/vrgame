using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumKitController : MonoBehaviour {

    public static DrumKitController Instance;

    public Indicators hihat;
    public Indicators snare;
    public Indicators bass;

    public Indicators hiTom;    
    public Indicators midTom;
    public Indicators floorTom;

    public Indicators sidecymbal;
    public Indicators topcymbal;

    void Awake () {
        if (Instance == null) Instance = GetComponent<DrumKitController>();
	}
	
    public void RestartBeat()
    {
        float nextPlayTime = Time.time + 5;

        hihat.nextPlayTime = nextPlayTime;
        snare.nextPlayTime = nextPlayTime;
        bass.nextPlayTime = nextPlayTime;
        hiTom.nextPlayTime = nextPlayTime;
        midTom.nextPlayTime = nextPlayTime;
        floorTom.nextPlayTime = nextPlayTime;
        sidecymbal.nextPlayTime = nextPlayTime;
        topcymbal.nextPlayTime = nextPlayTime;

        hihat.currentNote = 0;
        snare.currentNote = 0;
        bass.currentNote = 0;
        hiTom.currentNote = 0;
        midTom.currentNote = 0;
        floorTom.currentNote = 0;
        sidecymbal.currentNote = 0;
        topcymbal.currentNote = 0;

    }
}
