using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRCubeMoveTest : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean touchPad;
    public SteamVR_Action_Boolean holding;
    public SteamVR_Action_Boolean openDrumSet;
    public GameObject drumSetObj;
    
    public GameObject obj;

    private FixedJoint fixedJoint;
    void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();
    }
    void Update()
    {
        if (handType == SteamVR_Input_Sources.RightHand)
        {
            HoldObj();
            PickUpObj();
        }
        if(openDrumSet.GetState(handType))
        {
            if(!drumSetObj.activeSelf)
                drumSetObj.SetActive(true);
            else
                drumSetObj.SetActive(false);
        }
    }
    void PickUpObj()
    {
        if (obj != null)
        {
            fixedJoint.connectedBody = obj.GetComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            fixedJoint.connectedBody = null;
        }
    }
    void HoldObj()
    {
        if (holding.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                obj = hit.collider.gameObject;
            }
        }
        else
        {
            if (obj != null)
                obj.GetComponent<Rigidbody>().isKinematic = true;
            obj = null;
        }
    }
}
