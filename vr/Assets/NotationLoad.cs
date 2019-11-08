using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotationLoad : MonoBehaviour {

    public string hihat;
    public string snare;
    public string bass;
    public string hiTom;
    public string midTom;
    public string floorTom;
    public string sidecymbal;
    public string topcymbal;

    public void ChangeBeat()
    {
        DrumKitController.Instance.hihat.notation = hihat;
        DrumKitController.Instance.snare.notation = snare;
        DrumKitController.Instance.bass.notation = bass;
        DrumKitController.Instance.hiTom.notation = hiTom;
        DrumKitController.Instance.midTom.notation = midTom;
        DrumKitController.Instance.floorTom.notation = floorTom;
        DrumKitController.Instance.sidecymbal.notation = sidecymbal;
        DrumKitController.Instance.topcymbal.notation = topcymbal;
        DrumKitController.Instance.RestartBeat();

    }
}
