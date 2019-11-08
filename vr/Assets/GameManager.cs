using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    public GameObject DrumSet;
    public Vector3 offset;


    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean Click;

    void Start()
    {
        DrumSet.transform.position = Camera.allCameras[0].transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if(Click.GetState(handType))
        {
            DrumSet.transform.position = Camera.allCameras[0].transform.position + offset;
        }
    }
}
