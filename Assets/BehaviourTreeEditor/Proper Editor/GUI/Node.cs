using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

public class Node
{
   public Rect rect;
   public String title;
   public bool isDragged;
   public bool isSelected;

   public ConnectionPoint inPoint;
   public ConnectionPoint outPoint;
   public MonoScript tempScript = null;
   public string scriptPath = null;

   public GUIStyle style;
   public GUIStyle defaultNodeStyle;
   public GUIStyle selectedtNodeStyle;

    public Action<Node> OnRemoveNode;
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
                Action<Node> OnClickRemoveNode)
   {
       rect = new Rect(position.x, position.y, width, height);
       style = nodeStyle;
       inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
       outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
       defaultNodeStyle = nodeStyle;
       selectedtNodeStyle = selectedStyle;
       OnRemoveNode = OnClickRemoveNode;
   }

   public void Drag(Vector2 delta)
   {
       rect.position += delta;
   }

   public void Draw()
   {
        inPoint.Draw();
        outPoint.Draw();
        GUI.Box(rect, title, style);
        GUILayout.BeginArea(new Rect(rect.position.x + 10, rect.position.y, rect.width - 20, rect.height - 5));
        GUILayout.Space(10);
        tempScript = EditorGUILayout.ObjectField(tempScript, typeof(MonoScript), false) as MonoScript;
        if (tempScript != null && tempScript.GetClass().BaseType.Name == "BehaviourState")
        {
            scriptPath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(tempScript.GetClass().ToString())[0]);
        }            
        GUILayout.EndArea();

   }

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
                        style = selectedtNodeStyle;
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

   private void ProcessContextMenu()
   {
       GenericMenu genericMenu = new GenericMenu();
       genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
       genericMenu.ShowAsContext();
   }

   private void OnClickRemoveNode()
   {
       if (OnRemoveNode != null)
       {
           OnRemoveNode(this);
       }
   }
}
