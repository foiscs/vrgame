using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Node : IEquatable<Node>
{
    public int drumNum { get; set; }
    public float time { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        Node objAsNode = obj as Node;
        if (objAsNode == null)
            return false;
        else
            return base.Equals(objAsNode);
    }
    public bool Equals(Node other)
    {
        if (other == null)
            return false;
        return (this.time.Equals(other.time));
    }
}