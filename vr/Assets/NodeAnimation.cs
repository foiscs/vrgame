using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAnimation : MonoBehaviour
{
    public float time = 0;
    public float speed;
    
    public Vector3 StartVector;
    private Vector3 EndVector;
    private void Start()
    {
        transform.localRotation = transform.parent.localRotation;
        StartVector = transform.localScale;
        EndVector = new Vector3(0, StartVector.y, 0);
        time = 0;
    }
    private void OnEnable()
    {
        time = 0;
    }
    void Update()
    {
        transform.localScale = Vector3.Lerp(StartVector, EndVector, time);
        time += Time.deltaTime;

        if (time >= 1)
        {
            gameObject.SetActive(false);
            transform.localScale = StartVector;
        }
    }
}
