using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAnimation : MonoBehaviour
{
    [HideInInspector]
    public int num;
    public float speed;

    float startScaleX;
    private void Start()
    {
        transform.localRotation = transform.parent.rotation;
    }
    private void OnEnable()
    {
        startScaleX = transform.localScale.x;
    }
    void Update()
    {
        Vector3 a = new Vector3(1, 0, 1) * Time.deltaTime * speed;
        transform.localScale -= a;
        if (transform.localScale.x <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
