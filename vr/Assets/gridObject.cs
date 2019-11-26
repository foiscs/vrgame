using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridObject : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Vector3> objectsPos;

    void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if(!objects[i].activeSelf)
            {
                objects.RemoveAt(i);
                for (int j = 0; j < objects.Count; j++)
                {
                    objects[j].transform.localPosition = objectsPos[j];
                }
                break;
            }
        }
    }
}
