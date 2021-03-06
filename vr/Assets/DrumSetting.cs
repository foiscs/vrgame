﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DrumSetting : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Color32 NormalColor = Color.white;
    public Color32 HoverColor = Color.grey;
    public Color32 DownColor = Color.white;

    public GameObject drumObject;
    public GameObject drumPart;
    private Image image = null;
    private GameObject parent;
    private void Awake()
    {
        image = GetComponent<Image>();
        parent = GameObject.Find("Controller (right)");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TTS>().ReadText();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = HoverColor;
        Debug.Log("down");

        GameObject temp = Instantiate(drumPart,parent.transform);
        temp.transform.localPosition = new Vector3(0,0,0.5f);
        temp.GetComponent<BoxCollider>().enabled = true;
        drumObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
