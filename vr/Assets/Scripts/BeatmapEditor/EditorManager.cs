﻿using Valve.VR;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EditorManager : MonoBehaviour
{
    public float bpm;
    public float divider;
    public float beatCount;
    public float beatInterval;

    public Transform parent;
    public GameObject prefeb;
    public AudioSource audioSource;

    public float startTime;

    public AudioSource hihat;
    public AudioSource snare;
    public AudioSource bass;
    public AudioSource hiTom;
    public AudioSource midTom;
    public AudioSource floorTom;
    public AudioSource sidecymbal;
    public AudioSource topcymbal;



    public float[] Line_X;
    DirectoryInfo dir;
    [HideInInspector]
    public int dirCount;
    public GameObject MusicSelectBlock;
    public Transform MusicSelectBlockParent;

    public Canvas MusicSelectCanvas;
    public Canvas NodeAddCanvas;
    GameObject Line;
    GraphicRaycaster gr;
    PointerEventData ped;

    private void Awake()
    {
        dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        dirCount = dir.GetFiles().Length;

        foreach(var item in dir.GetDirectories())
        {
            GameObject temp = Instantiate(MusicSelectBlock,MusicSelectBlockParent);
            Transform _child = temp.transform.GetChild(0);
            _child.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(item.Name);
            DirectoryInfo file = new DirectoryInfo(Application.dataPath + "/Resources/Music/" + item.Name);
            foreach (var fileItem in file.GetFiles())
            {
                if(fileItem.Extension == ".png" || fileItem.Extension == ".jpg")
                {
                    _child.GetChild(0).GetComponent<Image>().sprite = LoadNewSprite(fileItem.FullName, 150, SpriteMeshType.Tight);
                }
            }

        }
        MusicSelectBlockParent.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 160 * dirCount);


    }
    private void Start()
    {
        Line = GameObject.Find("Line");
        Line.SetActive(false);
        gr = MusicSelectCanvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        MusicSelectCanvas.gameObject.SetActive(true);
        NodeAddCanvas.gameObject.SetActive(false);
    }
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
                audioSource.clip = Resources.Load<AudioClip>("Music/" + musicName + "/" + musicName);
                MusicSelectCanvas.gameObject.SetActive(false);
                NodeAddCanvas.gameObject.SetActive(true);
                Line.SetActive(true);
                bpm = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
                GameObject.Find("Metronome").GetComponent<Metronome_Lesson3>().bpm = bpm;
                beatCount = (float)bpm / divider;
                beatInterval = 1 / beatCount;

            }
        }

        if (NodeAddCanvas.gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.End))
            {
                MusicSelectCanvas.gameObject.SetActive(true);
                NodeAddCanvas.gameObject.SetActive(false);

                NodeList.Instance.nodes.Clear();
                NodeList.Instance.nodesPlayOne.Clear();
            }
            inputKey();
            nodeMove();
        }

        if (Input.GetKey(KeyCode.S) && GameObject.Find("Node1"))
            saveNodes(audioSource.clip.name, GameObject.Find("Node1"));
        else if(Input.GetKey(KeyCode.S))
            saveNodes(audioSource.clip.name, parent.gameObject);
    }   
    void inputKey()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addNodes(0);
            playNodeSound(0);
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.N))
        {
            addNodes(1);
            playNodeSound(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            addNodes(2);
            playNodeSound(2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            addNodes(3);
            playNodeSound(3);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            addNodes(4);
            playNodeSound(4);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            addNodes(5);
            playNodeSound(5);
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            addNodes(6);
            playNodeSound(6);
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            addNodes(7);
            playNodeSound(7);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = audioSource.time;
        }

        if(Input.GetKey(KeyCode.A))
        {
            NodeList.Instance.nodes.Clear();
            NodeList.Instance.nodesPlayOne.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (NodeAddCanvas.gameObject.activeSelf)
            {
                NodeList.Instance.nodes.Clear();
                NodeList.Instance.nodesPlayOne.Clear();
                SceneManager.LoadScene(1);
            }
            else
            {
                NodeList.Instance.nodes.Clear();
                NodeList.Instance.nodesPlayOne.Clear();
                SceneManager.LoadScene(0);
            }
        }
    }
    void addNodes(int num)
    {
        Node node = new Node();
        node.drumNum = num;
        node.time = audioSource.time;
        node.play = false;

        NodeList.Instance.nodes.Add(node);

        GameObject temp = Instantiate(prefeb, parent);
        temp.GetComponent<box>().drumNum = num;
        temp.transform.localPosition = new Vector3(Line_X[num], node.time);
    }
    public void save()
    {
        saveNodes(audioSource.clip.name);
    }
    public void load()
    {
        LoadNodes(audioSource.clip.name);
    }

    public void saveNodes(string audioName)
    {
        Debug.Log("dsa");
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        string path = "";
        foreach (var item in dir.GetDirectories())
        {
            if (item.Name == audioName)
            {
                path = item.FullName + "/" + audioName + ".beatmap";
            }
        }
        BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
        bw.Write(startTime);
        for (int i = 0; i < NodeList.Instance.nodes.Count; i++)
        {
            bw.Write(NodeList.Instance.nodes[i].drumNum);
            bw.Write(NodeList.Instance.nodes[i].time);
        }
        bw.Close();
    }
    public void saveNodes(string audioName, GameObject parent)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        string path = "";
        foreach (var item in dir.GetDirectories())
        {
            if (item.Name == audioName)
            {
                path = item.FullName + "/" + audioName + ".beatmap";
            }
        }
        BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
        bw.Write(startTime);
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            bw.Write(parent.transform.GetChild(i).GetComponent<box>().drumNum);
            bw.Write(parent.transform.GetChild(i).GetComponent<box>().oldPosY);
            //bw.Write(NodeList.Instance.nodes[i].drumNum);
            //bw.Write(NodeList.Instance.nodes[i].time);
        }
        bw.Close();
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
            startTime = br.ReadInt32();
            while (true)
            {
                try
                {
                    Node node = new Node();
                    node.drumNum = br.ReadInt32();
                    Debug.Log(node.drumNum);
                    node.time = br.ReadSingle();
                    node.play = false;
                    NodeList.Instance.nodes.Add(node);

                    GameObject temp = Instantiate(prefeb,parent);
                    temp.transform.localPosition = new Vector3(Line_X[node.drumNum], node.time);
                    temp.GetComponent<box>().num = NodeList.Instance.nodes.Count - 1;
                    temp.GetComponent<box>().drumNum = node.drumNum;
                }
                catch (EndOfStreamException e)
                {
                    br.Close();
                    break;
                }
            }
        }
    }
    public void nodeMove()
    {
        parent.transform.position = new Vector3(0, -audioSource.time);

        if (NodeList.Instance.nodes.Count < 1)
            return;
        for (int i = 0; i < NodeList.Instance.nodes.Count; i++)
        {
            if (audioSource.time >= NodeList.Instance.nodes[i].time && !NodeList.Instance.nodes[i].play)
            {
                NodeList.Instance.nodes[i].play = true;
                playNodeSound(NodeList.Instance.nodes[i].drumNum);
            }
            else if(audioSource.time < NodeList.Instance.nodes[i].time)
            {
                NodeList.Instance.nodes[i].play = false;
            }
        }

    }

    public void playNodeSound(int type)
    {
        if (type == 0)
        {
            bass.PlayOneShot(bass.clip);
        }
        if (type == 1)
        {
            floorTom.PlayOneShot(floorTom.clip);
        }
        if (type == 2)
        {
            snare.PlayOneShot(snare.clip);
        }
        if (type == 3)
        {
            hihat.PlayOneShot(hihat.clip);
        }
        if (type == 4)
        {
            midTom.PlayOneShot(midTom.clip);
        }
        if (type == 5)
        {
            hiTom.PlayOneShot(hiTom.clip);
        }
        if (type == 6)
        {
            sidecymbal.PlayOneShot(sidecymbal.clip);
        }
        if (type == 7)
        {
            topcymbal.PlayOneShot(topcymbal.clip);
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
