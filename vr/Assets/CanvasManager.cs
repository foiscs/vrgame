using Valve.VR;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class CanvasManager : MonoBehaviour
{
    DirectoryInfo dir;
    [HideInInspector]
    public int dirCount;
    public GameObject MusicSelectBlock;
    public Transform MusicSelectBlockParent;

    public Canvas MusicSelectCanvas;
    GameObject Line;
    GraphicRaycaster gr;
    PointerEventData ped;

    private void Awake()
    {
        dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        dirCount = dir.GetFiles().Length;

        foreach (var item in dir.GetDirectories())
        {
            GameObject temp = Instantiate(MusicSelectBlock, MusicSelectBlockParent);
            Transform _child = temp.transform.GetChild(0);
            _child.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(item.Name);
            DirectoryInfo file = new DirectoryInfo(Application.dataPath + "/Resources/Music/" + item.Name);
            foreach (var fileItem in file.GetFiles())
            {
                if (fileItem.Extension == ".png" || fileItem.Extension == ".jpg")
                {
                    _child.GetChild(0).GetComponent<Image>().sprite = LoadNewSprite(fileItem.FullName, 150, SpriteMeshType.Tight);
                }
            }

        }
        MusicSelectBlockParent.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 160 * dirCount);

    }
    void Start()
    {
        gr = MusicSelectCanvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }

    // Update is called once per frame
    void Update()
    {
        ped.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        gr.Raycast(ped, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            GameObject obj = raycastResults[i].gameObject;
            if (obj.name == "MusicSelectBlock(Clone)" && Input.GetMouseButtonUp(0))
            {
                string musicName = obj.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                
            }
        }
    }
    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }
    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
}
