using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DrumInfo : MonoBehaviour
{
    public Transform pos;
    public Vector3 scale;
    public int num;
    private void Start()
    {
        setNodePosition();
    }
    public void setNodePosition()
    {
        GameManager.Instance.nodesPosition[num] = pos.position;
        GameManager.Instance.nodesRotation[num] = transform.rotation;
        scale.x += 0.2f;
        scale.z += 0.2f;
        GameManager.Instance.nodesScale[num] = scale;
    }
}
