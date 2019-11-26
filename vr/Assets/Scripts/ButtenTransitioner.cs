using System.IO;
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

    public Image thumbnail;
    private Image image = null;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            PlyButton();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
        GetComponent<TTS>().ReadText();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = NormalColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = DownColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
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
    public void PlyButton()
    {
        for (int i = 0; i < GameManager.Instance.transform.childCount; i++)
        {
            GameObject child = GameManager.Instance.transform.GetChild(i).gameObject;

            child.GetComponent<BoxCollider>().enabled = false;
        }
        LoadNodes(GameManager.Instance.musicName);

        SceneManager.LoadScene(2);
    }
    public void LoadNodes(string audioName)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        string path = "";
        foreach (var item in dir.GetDirectories())
        {
            if (item.Name == audioName)
                path = item.FullName + "/" + audioName + ".beatmap";
        }
        if (File.Exists(path))
        {
            BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
            int startTime = br.ReadInt32();
            while (true)
            {
                try
                {
                    Node node = new Node();
                    node.drumNum = br.ReadInt32();
                    node.time = br.ReadSingle();
                    node.play = false;
                    GameManager.Instance.nodes.Add(node);
                    GameManager.Instance.nodesPlayOne.Add(false);
                }
                catch (EndOfStreamException e)
                {
                    br.Close();
                    break;
                }
            }
        }
    }
    public void ThumbnailChange()
    {
        GameObject.Find("Thumbnail").GetComponent<Image>().sprite = thumbnail.sprite;
    }
}
