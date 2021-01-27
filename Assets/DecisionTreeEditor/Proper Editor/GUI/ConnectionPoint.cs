using System;
using UnityEngine;

public enum ConnectionPointType {In, Out}
public class ConnectionPoint
{
    /* Variable: rect
     * Rect used to draw Node
     */
    public Rect rect;

    /* Variable: type
     * Determines the type of the ConnectionPoint
     * Its either in or out
     */
    public ConnectionPointType type;

    /* Variable: node
     * Defines the parent Node
     */
    public Node node;

    /* Variable: style
     * Defines the current style of the ConnectionPoint
     */
    public GUIStyle style;

    /* Variable: OnClickConnectionPoint
     * Handles clicking on ConnectionPoint
     */
    public Action<ConnectionPoint> OnClickConnectionPoint;

    /* Function: ConnectionPoint
     * Constructor
     */
    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 10f, 20f);
    }

    /* Function: Draw
     * Handles drawing ConnectionPoint
     */
    public void Draw()
    {
        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

        switch(type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;
            
            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }

        if (GUI.Button(rect, "", style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}
