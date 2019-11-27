using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();

    public Transform[] nodesPosition;
    public GameObject nodeAnimationPrefeb;

    public TextMeshProUGUI textMeshPro;
    public static GameManager Instance = null;

    public int Level = 1;
    public string musicName = null;

    private List<GameObject> nodeObjList;
    private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        nodesPosition = new Transform[8];
    }
    private void Update()
    {
        changeLevelText();
        PlayNodes();
    }
    void changeLevelText()
    {
        if (textMeshPro == null)
            return;
        if (Level == 1)
            textMeshPro.text = "EASY";
        if (Level == 2)
            textMeshPro.text = "NORMAL";
        if (Level == 3)
            textMeshPro.text = "HEAD";
        if (Level == 4)
            textMeshPro.text = "EXPERT";
        if (Level == 5)
            textMeshPro.text = "EXPERT+";
    }
    void PlayNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if(audioSource.time > nodes[i].time && !nodesPlayOne[i])
            {
                nodesPlayOne[i] = true;
                
                GameObject nodeObj = CreateNodeAnimation();
                nodeObj.transform.position = nodesPosition[nodes[i].drumNum].position;
                nodeObj.transform.rotation = nodesPosition[nodes[i].drumNum].rotation;
                nodeObj.transform.localScale = nodesPosition[nodes[i].drumNum].localScale;
                //노드 생성
            }
        }
    }

    public void LoadMusicInPlayScene()
    {
        audioSource = GameObject.Find("AudioPeer").GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/" + musicName + "/" + musicName);
    }
    public GameObject CreateNodeAnimation()
    {
        for (int i = 0; i < nodeObjList.Count; i++)
        {
            if(!nodeObjList[i].activeSelf)
            {
                nodeObjList[i].SetActive(true);
                return nodeObjList[i];
            }
        }
        return Instantiate(nodeAnimationPrefeb);
    }
    public void SetNodesPosition(string name, Transform transform)
    {
        if(name.Contains("Floor"))
        {
            nodesPosition[1].position = transform.position;
            nodesPosition[1].rotation = transform.rotation;
            nodesPosition[1].localScale = new Vector3(transform.localScale.x+0.2f, 1, transform.localScale.x + 0.2f);
        }
    }
}


