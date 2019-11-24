using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();

    public static GameManager Instance = null;
    public string musicName = null;
    public int Level = 1;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}


