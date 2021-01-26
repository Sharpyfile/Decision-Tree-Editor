using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class DecisionTreePrefab : ScriptableObject
{
    public List<ConnectionContainer> connectionContainers;
    public List<NodeContainer> nodeContainers;
    public int originalNodeIndex;
}
