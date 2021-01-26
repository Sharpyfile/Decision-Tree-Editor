using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTreeEditorPrefab : ScriptableObject
{
    public List<Connection> connections;
    public List<Node> nodes;
    public int originalNodeIndex;
}
