using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ButtenTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler,IPointerDownHandler, IPointerClickHandler
{
    public Color32 NormalColor = Color.white;
    public Color32 HoverColor = Color.grey;
    public Color32 DownColor = Color.white;

    public UnityEvent methods;

    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        image.color = HoverColor;
        GetComponent<TTS>().ReadText();
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
        methods.Invoke();
    }

    public void ScrollUpButton()
    {

    }
    public void ScrollDownButton()
    {

    }
    public void SelectLevel(int value)
    {
        GameManager.Instance.Level = value;
    }
    public void SelectMusic()
    {
        GameManager.Instance.musicName= transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
    }
}
