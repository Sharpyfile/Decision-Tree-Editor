using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviourTreeEditor : EditorWindow
{  
    public static BehaviourTreePrefab behaviourTreePrefab;
    public string newBehaviourTreePrefabName = "";
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
        BehaviourTreeEditor window = GetWindow<BehaviourTreeEditor>();
        window.titleContent = new GUIContent("Node Based Editor");
    }

    public void OnEnable()
    {
        behaviourTreePrefab = (BehaviourTreePrefab)ScriptableObject.CreateInstance(typeof(BehaviourTreePrefab));

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
        if (GUILayout.Button("Save behaviour tree"))
            SaveBehaviourTreePrefab();
        GUILayout.Button("Load");
        GUILayout.Button("New");
        EditorGUILayout.EndHorizontal();      

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name of current Behaviour Tree");
        newBehaviourTreePrefabName = EditorGUILayout.TextField(newBehaviourTreePrefabName); 
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

    public void SaveBehaviourTreePrefab()
    {
        string behaviourTreePrefabPath = "Assets/Recources/BehaviourTrees/" + newBehaviourTreePrefabName;
        BehaviourTreePrefab newBehaviourTreePrefab = (BehaviourTreePrefab)ScriptableObject.CreateInstance(typeof(BehaviourTreePrefab));
        AssetDatabase.CreateAsset(newBehaviourTreePrefab, behaviourTreePrefabPath + ".asset");
        newBehaviourTreePrefab = (BehaviourTreePrefab)AssetDatabase.LoadAssetAtPath(behaviourTreePrefabPath + ".asset", typeof(BehaviourTreePrefab));
        EditorUtility.SetDirty(newBehaviourTreePrefab);

        newBehaviourTreePrefab.nodes = new List<Node>(this.nodes);      
        newBehaviourTreePrefab.connections = new List<Connection>(this.connections);      
        newBehaviourTreePrefab.originalNodeIndex = this.originalNodeIndex;
        newBehaviourTreePrefab.testing = new List<string>();
        newBehaviourTreePrefab.testing.Add(connections[0].intBasedConditions[0].ToString());   
        for(int i = 0; i < newBehaviourTreePrefab.connections.Count; i++)
        {
            newBehaviourTreePrefab.connections[i].ConvertAllConditionsToString();
            Debug.Log(newBehaviourTreePrefab.connections[i].previousNodeID);
            Debug.Log(newBehaviourTreePrefab.connections[i].nextNodeID);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
