using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRCubeMoveTest : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean touchPad;
    public SteamVR_Action_Boolean holding;

    public SteamVR_TrackedObject trackedObject;
    public GameObject obj;

    private FixedJoint fixedJoint;
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        fixedJoint = GetComponent<FixedJoint>();
    }
    void Update()
    {
        if (holding.GetState(handType))
        {
            lineRenderer.enabled = true;
            RaycastHit hit;
            lineRenderer.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                lineRenderer.SetPosition(1, hit.point);
                obj = hit.collider.gameObject;
            }
            else
                lineRenderer.SetPosition(1, transform.TransformDirection(Vector3.forward) * Mathf.Infinity);
        }
        else
        {
            lineRenderer.enabled = false;
            if(obj != null)
                obj.GetComponent<Rigidbody>().isKinematic = true;
            obj = null;
        }
        PickUpObj();
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
}
