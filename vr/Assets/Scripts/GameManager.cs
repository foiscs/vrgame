using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();

    public Vector3[] nodesPosition;
    public Quaternion[] nodesRotation;
    public Vector3[] nodesScale;
    public GameObject nodeAnimationPrefeb;

    public TextMeshProUGUI textMeshPro;
    public static GameManager Instance = null;

    public int Level = 1;
    public string musicName = null;

    [HideInInspector]
    public List<GameObject> nodeObjList = new List<GameObject>();
    private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        nodesPosition = new Vector3[8];
        nodesRotation = new Quaternion[8];
        nodesScale = new Vector3[8];

    }
    private void Start()
    {
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
        Debug.Log(nodes.Count);
        for (int i = 0; i < nodes.Count; i++)
        {
            if(audioSource.time > nodes[i].time && !nodesPlayOne[i])
            {
                nodesPlayOne[i] = true;
                
                GameObject nodeObj = CreateNodeAnimation();
                nodeObj.transform.position = nodesPosition[nodes[i].drumNum];
                nodeObj.transform.rotation = nodesRotation[nodes[i].drumNum];
                nodeObj.transform.localScale = nodesScale[nodes[i].drumNum];
                nodeObj.GetComponent<NodeAnimation>().num = nodes[i].drumNum;
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
        return Instantiate(nodeAnimationPrefeb,transform);
    }
    
}


