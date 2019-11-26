using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DrumSetting : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Color32 NormalColor = Color.white;
    public Color32 HoverColor = Color.grey;
    public Color32 DownColor = Color.white;

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
        temp.transform.localPosition = Vector3.forward;
        temp.transform.localScale = Vector3.one;
        temp.GetComponent<BoxCollider>().enabled = true;
        drumPart.SetActive(false);
        gameObject.SetActive(false);
    }
}
