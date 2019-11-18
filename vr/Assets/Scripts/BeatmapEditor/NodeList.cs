using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public static NodeList Instance = null;

    public List<Node> nodes = new List<Node>();
    public List<bool> nodesPlayOne = new List<bool>();
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
