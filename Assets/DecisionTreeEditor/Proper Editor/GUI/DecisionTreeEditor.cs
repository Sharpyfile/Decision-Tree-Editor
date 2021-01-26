using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DecisionTreeEditor : EditorWindow
{  
    public DecisionTreePrefab DecisionTreePrefab = null;
    public string newDecisionTreePrefabName = "";
    [SerializeField]
    public List<Node> nodes;
    [SerializeField]
    public List<Connection> connections;
    public GUIStyle nodeStyle;
    public GUIStyle originalNodeStyle;
    public GUIStyle selectedNodeStyle;
    public GUIStyle originalSelectedNodeStyle;
    public GUIStyle inPointStyle;
    public GUIStyle outPointStyle;
    [SerializeField]
    public Node originalNode;
    public int originalNodeIndex;
    public ConnectionPoint selectedInPoint;
    public ConnectionPoint selectedOutPoint;
    public Vector2 offset;
    public Vector2 drag;
    int nextNodeID = 0;

    [MenuItem("Window/Node Based Editor")]
    public static void OpenWindow()
    {
        DecisionTreeEditor window = GetWindow<DecisionTreeEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }

    public void OnEnable()
    {
        DecisionTreePrefab = (DecisionTreePrefab)ScriptableObject.CreateInstance(typeof(DecisionTreePrefab));

        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        originalNodeStyle = new GUIStyle();
        originalNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2.png") as Texture2D;
        originalNodeStyle.border = new RectOffset(12, 12, 12, 12);

        originalSelectedNodeStyle = new GUIStyle();
        originalSelectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2 on.png") as Texture2D;
        originalSelectedNodeStyle.border = new RectOffset(12, 12, 12, 12);


        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);
 
        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    public void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        EditorGUILayout.BeginHorizontal();
        

        EditorGUIUtility.labelWidth = 10;
        EditorGUILayout.LabelField("Decision tree to load");
        EditorGUIUtility.labelWidth = 0;
        DecisionTreePrefab = EditorGUILayout.ObjectField(DecisionTreePrefab, typeof(ScriptableObject), true) as DecisionTreePrefab;
        if (GUILayout.Button("Load"))
        {
            if (this.nodes != null)
            {
                this.nodes.Clear();
            }
            else
            {
                this.nodes = new List<Node>();
            }
            
            foreach(Node node in DecisionTreePrefab.nodes)
            {
                nodes.Add(new Node(node.rect.position, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnMarkAsOriginalNode, nextNodeID.ToString()));
                nodes[nodes.Count - 1].tempScript = node.tempScript;
                nextNodeID++;
            }
            
            if (this.connections != null)
            {
                this.connections.Clear();
            }
            else
            {
                this.connections = new List<Connection>();
            }
            foreach(Connection connection in DecisionTreePrefab.connections)
            {
                Connection tempConnection = new Connection(nodes[int.Parse(connection.nextNodeID)].inPoint, nodes[int.Parse(connection.previousNodeID)].outPoint, OnClickRemoveConnection);
                ConditionValidator.ConvertStringToConditions(tempConnection, connection);
                tempConnection.previousNodeID = connection.previousNodeID;
                tempConnection.nextNodeID = connection.nextNodeID;
                tempConnection.connectionTrait = connection.connectionTrait;
                connections.Add(tempConnection);
            }    
            originalNode = this.nodes[DecisionTreePrefab.originalNodeIndex];
            originalNode.defaultNodeStyle = originalNodeStyle;
            originalNode.selectedtNodeStyle = originalSelectedNodeStyle;
            GUI.changed = true;   
            Repaint();     
        }
        

        if (GUILayout.Button("New"))
        {
            this.nodes.Clear();
            this.connections.Clear();
            this.originalNode = null;
            GUI.changed = true;
        }
        EditorGUIUtility.labelWidth = 5;
        EditorGUILayout.LabelField("Name of tree");
        EditorGUIUtility.labelWidth = 0;
        newDecisionTreePrefabName = EditorGUILayout.TextField(newDecisionTreePrefabName); 
        if (GUILayout.Button("Save decision tree"))
            SaveDecisionTreePrefab();
        EditorGUILayout.EndHorizontal();     
        DrawNodes();
        DrawConnections();        

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed)
            Repaint();
    }

    public void DrawNodes()
    {
        if (nodes != null)
        {
            foreach( Node node in nodes)
            {
                node.Draw();
                
            }
        }
    }

    public void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    public void DrawConnections()
    {
        if (connections != null)
        {
            foreach(Connection connection in connections)
            {
                connection.Draw();
            }
        }
    }

    public void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    public void ProcessEvents(Event e)
    {
        drag = Vector2.zero;
        switch(e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        
            default:
                break;
        }
    }

    public void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    public void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    public void OnClickAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<Node>();
        }
        nodes.Add(new Node(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, OnMarkAsOriginalNode, nextNodeID.ToString()));
        nextNodeID++;
    }

    public void OnMarkAsOriginalNode(Node node)
    {
        if (originalNode != null && originalNode != node)
        {
            nodes.Find(x => x == originalNode).defaultNodeStyle = nodeStyle;
            nodes.Find(x => x == originalNode).selectedtNodeStyle = selectedNodeStyle;
            originalNode = node;
            originalNodeIndex = nodes.FindIndex(x => x == node);
            originalNode.defaultNodeStyle = originalNodeStyle;
            originalNode.selectedtNodeStyle = originalSelectedNodeStyle;

        }
        else
        {
            originalNode = node;
            originalNode.defaultNodeStyle = originalNodeStyle;
            originalNode.selectedtNodeStyle = originalSelectedNodeStyle;
        }
        
    }

    public void OnClickRemoveNode(Node node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            foreach (Connection connection in connections)
            {
                if (connection.inPoint == node.inPoint || connection.outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connection);
                }
            }

            foreach (Connection connection in connectionsToRemove)
            {
                connections.Remove(connection);
            }

            // Keep us safe from that bad boi that is memory leak
            connectionsToRemove = null;
        }

        nodes.Remove(node);       
        
    }

    public void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    public void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedInPoint.node != selectedOutPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    public void OnClickRemoveConnection(Connection connection)
    {
        connections.Remove(connection);
    }

    public void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (nodes != null)
        {
            foreach(Node node in nodes)
            {
                node.Drag(delta);
            }
        }

        GUI.changed = true;
    }

    public void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }
        Connection tempConnection = new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection);
        connections.Add(tempConnection);
        tempConnection.nextNodeID = selectedInPoint.node.nodeID;
        Debug.Log(selectedInPoint.node.nodeID);
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].outPoint  == selectedOutPoint)
            {
                tempConnection.previousNodeID = nodes[i].nodeID;
                nodes[i].connections.Add(tempConnection);
                return;
            }
        }
    }

    public void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    public void SaveDecisionTreePrefab()
    {
        string DecisionTreePrefabPath = "Assets/Recources/DecisionTrees/" + newDecisionTreePrefabName;
        DecisionTreePrefab newDecisionTreePrefab = (DecisionTreePrefab)ScriptableObject.CreateInstance(typeof(DecisionTreePrefab));
        AssetDatabase.CreateAsset(newDecisionTreePrefab, DecisionTreePrefabPath + ".asset");
        newDecisionTreePrefab = (DecisionTreePrefab)AssetDatabase.LoadAssetAtPath(DecisionTreePrefabPath + ".asset", typeof(DecisionTreePrefab));
        EditorUtility.SetDirty(newDecisionTreePrefab);

        newDecisionTreePrefab.nodes = new List<Node>(this.nodes);      
        newDecisionTreePrefab.connections = new List<Connection>(this.connections);      
        newDecisionTreePrefab.originalNodeIndex = this.originalNodeIndex;
        newDecisionTreePrefab.testing = new List<string>();
        for(int i = 0; i < newDecisionTreePrefab.connections.Count; i++)
        {
            newDecisionTreePrefab.connections[i].ConvertAllConditionsToString();
            Debug.Log(newDecisionTreePrefab.connections[i].previousNodeID);
            Debug.Log(newDecisionTreePrefab.connections[i].nextNodeID);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
