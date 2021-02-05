using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTreeEditorPrefab : ScriptableObject
{
    /* Variable: connections
     * Holds a List of Connection
     */
    public List<Connection> connections;

    /* Variable: nodes
     * Holds a list of Node
     */
    public List<Node> nodes;

    /* Variable: originalNodeIndex
     * Holds an index of the original Node
     */
    public int originalNodeIndex;
}
