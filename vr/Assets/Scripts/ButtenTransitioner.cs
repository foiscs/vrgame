using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtenTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler,IPointerDownHandler, IPointerClickHandler
{
    public Color32 NormalColor = Color.white;
    public Color32 HoverColor = Color.grey;
    public Color32 DownColor = Color.white;

    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        image.color = HoverColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        image.color = NormalColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("enter");
        image.color = DownColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        image.color = HoverColor;
    }
}
