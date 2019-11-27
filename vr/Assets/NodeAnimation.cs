using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAnimation : MonoBehaviour
{
    [HideInInspector]
    public int num;
    public float speed;
    
    private void Start()
    {
        transform.localRotation = transform.parent.rotation;
    }
    void Update()
    {
        transform.localScale -= new Vector3(1, 0, 1) * Time.deltaTime * speed;
        

        if (transform.localScale.x <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
