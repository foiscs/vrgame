using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class RecodingNode : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public AudioSource audioSource;
    
    DirectoryInfo dir;
    private void Awake()
    {
        dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addNodes(0);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            addNodes(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            addNodes(2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            addNodes(3);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            addNodes(4);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            addNodes(5);
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            addNodes(6);
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            addNodes(7);
        }

    }
    void addNodes(int num)
    {
        Node node = new Node();
        node.drumNum = num;
        node.time = audioSource.time;

        if (nodes.Count == 0 || !nodes.Exists(x => x.time > node.time))
            nodes.Add(node);
        else
        {
            nodes.IndexOf(node, nodes.FindLastIndex(x => x.time <= node.time) + 1);
        }
        Debug.Log(nodes.Count);
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
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
        string path="";
        foreach (var item in dir.GetDirectories())
        {
            if (item.Name == audioName)
                path = item.FullName + "/" + audioName + ".beatmap";
        }
        BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
        for (int i = 0; i < nodes.Count; i++)
        {
            bw.Write(nodes[i].drumNum);
            bw.Write(nodes[i].time);
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
            while(true)
            {
                try
                {
                    Node node = new Node();
                    node.drumNum = br.ReadInt32();
                    node.time = br.ReadSingle();
                }
                catch(EndOfStreamException e)
                {
                    br.Close();
                    break;
                }
            }
        }
    }

}
