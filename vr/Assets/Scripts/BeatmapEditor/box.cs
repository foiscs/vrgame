﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public int num;

    private float startPosX;
    private float startPosY;

    public float oldPosY;
    private bool isBeingHeld = false;
    private void Start()
    {
        oldPosY = transform.localPosition.y;
    }

    void Update()
    {
        if(isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(transform.position.x,mousePos.y-startPosY,0);
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }
    }
    private void OnMouseUp()
    {
        isBeingHeld = false;
        Debug.Log(NodeList.Instance.nodes[num].time);
        NodeList.Instance.nodes[num].time = transform.localPosition.y;
    }
}