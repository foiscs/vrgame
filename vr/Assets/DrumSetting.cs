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

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TTS>().ReadText();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = HoverColor;
        GameObject temp = Instantiate(drumPart,GameManager.Instance.transform);
    }
}
