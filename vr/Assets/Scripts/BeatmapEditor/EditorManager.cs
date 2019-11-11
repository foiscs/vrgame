using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Valve.VR;
public class EditorManager : MonoBehaviour
{
    public float bpm;
    public float divider;
    public float beatCount;
    public float beatInterval;

    public Transform parent;
    public GameObject prefeb;
    public AudioSource audioSource;

    public float[] Line_X;
    DirectoryInfo dir;
    private void Start()
    {
        bpm = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
        beatCount = (float)bpm / divider;
        beatInterval = 1 / beatCount;
        dir = new DirectoryInfo(Application.dataPath + "/Resources/Music");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addNodes(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
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

        nodeMove();
    }
    void addNodes(int num)
    {
        Node node = new Node();
        node.drumNum = num;
        node.time = audioSource.time;

        if (NodeList.Instance.nodes.Count == 0 || !NodeList.Instance.nodes.Exists(x => x.time > node.time))
            NodeList.Instance.nodes.Add(node);
        else
        {
            NodeList.Instance.nodes.IndexOf(node, NodeList.Instance.nodes.FindLastIndex(x => x.time <= node.time) + 1);
        }
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
        string path = "";
        foreach (var item in dir.GetDirectories())
        {
            if (item.Name == audioName)
                path = item.FullName + "/" + audioName + ".beatmap";
        }
        BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
        for (int i = 0; i < NodeList.Instance.nodes.Count; i++)
        {
            bw.Write(NodeList.Instance.nodes[i].drumNum);
            bw.Write(NodeList.Instance.nodes[i].time);
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
            while (true)
            {
                try
                {
                    Node node = new Node();
                    node.drumNum = br.ReadInt32();
                    node.time = br.ReadSingle();
                    NodeList.Instance.nodes.Add(node);

                    GameObject temp = Instantiate(prefeb,parent);
                    temp.transform.localPosition = new Vector3(Line_X[node.drumNum], node.time);
                    temp.GetComponent<box>().num = NodeList.Instance.nodes.Count - 1;
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
    }
}
