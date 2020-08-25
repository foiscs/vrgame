using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();
    public List<bool> nodeSoundPlayOne = new List<bool>();
    [HideInInspector]
    public Vector3[] nodesPosition;
    [HideInInspector]
    public Quaternion[] nodesRotation;
    [HideInInspector]
    public Vector3[] nodesScale;

    public List<List<GameObject>> nodesArray = new List<List<GameObject>>();
    public GameObject nodeAnimationPrefeb;

    public int Level = 1;
    public string musicName = null;

    [HideInInspector]
    public List<GameObject> nodeObjList = new List<GameObject>();
    public AudioSource audioSource;

    private GameObject drumSoundList;

    public static GameManager Instance = null;

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
        for (int i = 0; i < 8; i++)
        {
            nodesArray.Add(new List<GameObject>());
        }

        drumSoundList = GameObject.Find("SoundBox");
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if(!audioSource)
                LoadMusicInPlayScene();
            
            PlayNodes();
            PlayAuto();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < nodesArray[i].Count; j++)
                {
                    if(!nodesArray[i][j].activeSelf)
                    {
                        nodesArray[i].RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
    void PlayNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if(audioSource.time >= nodes[i].time-0.6f && !nodesPlayOne[i])
            {
                nodesPlayOne[i] = true;
                
                GameObject node = CreateNodeAnimation();
                int num = nodes[i].drumNum;
                node.transform.position = nodesPosition[num];
                node.transform.rotation = nodesRotation[num];
                node.transform.localScale = nodesScale[num];
                node.GetComponent<NodeAnimation>().num = num;
                nodesArray[num].Add(node);
                //노드 생성
            }
        }

    }
    void PlayAuto()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (Level == 0 &&audioSource.time >= nodes[i].time && !nodeSoundPlayOne[i])
            {
                nodeSoundPlayOne[i] = true;
                drumSoundList.transform.GetChild(nodes[i].drumNum).GetComponent<AudioSource>().Play();
            }
        }
    }
    public void HitDrum(int num)
    {
        if(nodesArray[num].Count!=0)
            nodesArray[num][0].SetActive(false);
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
        GameObject temp = Instantiate(nodeAnimationPrefeb, transform);
        nodeObjList.Add(temp);
        return temp;
    }
    
}


