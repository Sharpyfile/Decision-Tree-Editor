using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class BehaviourTreePrefab : ScriptableObject
{
    public List<string> testing;
    public List<Connection> connections;
    public List<Node> nodes;
    public int originalNodeIndex;
}
