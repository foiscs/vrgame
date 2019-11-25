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
            if(transform.childCount == 4 && holding.GetState(handType))
            {
                transform.GetChild(3).parent = GameManager.Instance.transform;
            }
        }
        if(openDrumSet.GetState(handType))
        {
            if(!drumSetObj.activeSelf)
                drumSetObj.SetActive(true);
            else
                drumSetObj.SetActive(false);
            GetComponent<TTS>().text = "드럼설정창" + drumSetObj.activeSelf;
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
