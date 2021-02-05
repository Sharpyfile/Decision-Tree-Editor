#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Node
{
    /* Variable: rect
     * Rect used to draw Node
     */
    public Rect rect;

    /* Variable: title
    * String used to draw Node
    */
    public String title;

    /* Variable: isDragged
    * Defines if node is being dragged
    */
    public bool isDragged;

    /* Variable: isSelected
    * Defines if node is selected
    */
    public bool isSelected;

    /* Variable: inPoint
    * ConnectionPoint that is on the left side of the node
    */
    public ConnectionPoint inPoint;

    /* Variable: outPoint
    * ConnectionPoint that is on the right side of the node
    */
    public ConnectionPoint outPoint;
#if UNITY_EDITOR

    /* Variable: tempScript
    * MonoScript of DecisionState that can be selected
    * Its then later used to determine classType
    */
    public MonoScript tempScript = null;
#endif

    /* Variable: classType
    * Name of the class of DecisionState
    */
    public string classType;

    /* Variable: nodeID
    * ID of the node
    */
    public string nodeID;

    /* Variable: connections
    * List of Connection that holds all connections that
    * goes out of the Node
    */
    public List<Connection> connections = new List<Connection>();

    /* Variable: style
    * Current style of the Node
    */
    public GUIStyle style;

    /* Variable: defaultNodeStyle
    * Default style of the Node
    */
    public GUIStyle defaultNodeStyle;

    /* Variable: selectedNodeStyle
    * Style of the Node if its selected
    */
    public GUIStyle selectedNodeStyle;

    /* Variable: OnRemoveNode
    * Action that is executed if you remove Node
    */
    public Action<Node> OnRemoveNode;

    /* Variable: OnMarkAsOriginalNode
    * Action that is executed if mark Node as original
    */
    public Action<Node> OnMarkAsOriginalNode;

    /* Function: Node
     * Constructor of the Node
     */
    public Node(
                    Vector2 position, 
                    float width, 
                    float height, 
                    GUIStyle nodeStyle, 
                    GUIStyle selectedStyle,
                    GUIStyle inPointStyle, 
                    GUIStyle outPointStyle, 
                    Action<ConnectionPoint> OnClickInPoint, 
                    Action<ConnectionPoint> OnClickOutPoint,
                    Action<Node> OnClickRemoveNode,
                    Action<Node> OnClickMarkAsOriginalNode,
                    string nodeID)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
        OnMarkAsOriginalNode = OnClickMarkAsOriginalNode;
        this.nodeID = nodeID;
    }
#if UNITY_EDITOR

    /* Function: Drag
     * Handles dragging of the Node
     */
    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    /* Function: Draw
     * Handles drawing the Node
     */
    public void Draw()
    {
            inPoint.Draw();
            outPoint.Draw();
            GUI.Box(rect, title, style);
            GUILayout.BeginArea(new Rect(rect.position.x + 10, rect.position.y, rect.width - 20, rect.height - 5));
            GUILayout.Space(10);
            tempScript = EditorGUILayout.ObjectField(tempScript, typeof(MonoScript), false) as MonoScript;
            if (tempScript != null && tempScript.GetClass().BaseType.Name == "DecisionState")
            {
                classType = tempScript.GetClass().ToString();
            }            
            GUILayout.EndArea();

    }

    /* Function: ProcessEvents
     * Takes Event and executes specific code
     * Handles selecting Nodes and dragging them 
     * Returns true on success
     */
    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
                case EventType.MouseDown:
                    if(e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                        }
                    }
                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;
                
                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
        }

        return false;
    }


    /* Function: ProcessContextMenu
     * Handles dropdown menu on the Right Mouse Button
     */
    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.AddItem(new GUIContent("Mark as original"), false, OnClickMarkAsOriginalNode);
        genericMenu.ShowAsContext();
    }

    /* Function: OnClickRemoveNode
     * Handles removing Node
     */
    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }

    /* Function: OnClickMarkAsOriginalNode
     * Handles marking Node as original
     */
    private void OnClickMarkAsOriginalNode()
    {
        if (OnMarkAsOriginalNode != null)
        {
            OnMarkAsOriginalNode(this);
        }
    }
    #endif
}
