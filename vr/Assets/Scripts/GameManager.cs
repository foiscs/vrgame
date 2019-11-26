using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();

    public TextMeshProUGUI textMeshPro;
    public static GameManager Instance = null;

    public int Level = 1;
    public string musicName = null;

    private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        changeLevelText();
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

                //노드 생성
            }
        }
    }

    public void LoadMusicInPlayScene()
    {
        audioSource = GameObject.Find("AudioPeer").GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/" + musicName + "/" + musicName);
    }
}


