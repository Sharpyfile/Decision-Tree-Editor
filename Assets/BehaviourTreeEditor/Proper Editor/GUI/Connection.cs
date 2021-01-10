using System;
using UnityEngine;
using UnityEditor;

public class Connection
{
    public ConnectionPoint inPoint;

    public ConnectionPoint outPoint;
    public Action<Connection> OnClickRemoveConnection;

    TypeOfCondition option;

    BehaviourStateConnection BSconnection;

    IntBasedCondition tempIntBasedCondition;
    FloatBasedCondition tempFloatBasedCondition;
    BoolBasedCondition tempBoolBasedCondition;
    StringBasedCondition tempStringBasedCondition;

    Vector2 scrollView;



    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.OnClickRemoveConnection = OnClickRemoveConnection;
        this.BSconnection = new BehaviourStateConnection(outPoint.node.scriptPath);
    }

    public void Draw()
    {
        Handles.DrawBezier(
            inPoint.rect.center,
            outPoint.rect.center,
            inPoint.rect.center + Vector2.left * 50f,
            outPoint.rect.center - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );

        Vector3 position = (inPoint.rect.center + outPoint.rect.center) * 0.5f;
        Rect rect = new Rect(position.x - 150, position.y - 50, 300f, 100f);
        
        GUI.Box(rect,"");
        GUILayout.BeginArea(new Rect(position.x - 150, position.y - 50, 300, 100));
        scrollView = GUILayout.BeginScrollView(scrollView, true, true, GUILayout.Width(300), GUILayout.Height(100));
        
        
        if (GUILayout.Button("Remove connection"))
        {
            if(OnClickRemoveConnection != null)
            {
                OnClickRemoveConnection(this);
            }
        }
        
        option = (TypeOfCondition)EditorGUILayout.EnumPopup("Type of new condition: ", option);
        
        for (int i = 0; i < BSconnection.IntBasedConditions.Count; i++)
        {
           ModifyIntCondition(BSconnection.IntBasedConditions[i]);
        }

        for (int i = 0; i < BSconnection.FloatBasedConditions.Count; i++)
        {
           ModifyFloatCondition(BSconnection.FloatBasedConditions[i]);
        }

        for (int i = 0; i < BSconnection.BoolBasedConditions.Count; i++)
        {
           ModifyBoolCondition(BSconnection.BoolBasedConditions[i]);
        }

        for (int i = 0; i < BSconnection.StringBasedConditions.Count; i++)
        {
           ModifyStringCondition(BSconnection.StringBasedConditions[i]);
        }
        
        if (GUILayout.Button("Add condition"))
        {
            switch(option)
            {
                case TypeOfCondition.Int:
                    AddIntCondition();
                    break;

                case TypeOfCondition.Float:
                    AddFloatCondition();
                    break;

                case TypeOfCondition.Bool:
                    AddBoolCondition();
                    break;

                case TypeOfCondition.String:
                    AddStringCondition();
                    break;
                
                default:
                    break;
            }
        }
        
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        
    }

    private void ModifyIntCondition(IntBasedCondition condition)
    {
        IntBasedCondition tempCondition = condition;
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        string newConditionName = tempCondition.conditionName;
        Operation newConditionOperation = tempCondition.operation;
        int newVariable1 = tempCondition.variable1;
        int newVariable2 = tempCondition.variable2;
        newConditionName = EditorGUILayout.TextField(newConditionName);
            
        newVariable2 = EditorGUILayout.IntField(newVariable2);
        EditorGUIUtility.labelWidth = 35;
        newConditionOperation = (Operation)EditorGUILayout.EnumPopup("INT" ,newConditionOperation, GUILayout.ExpandWidth(false));    
        EditorGUIUtility.labelWidth = 0;       
        IntBasedCondition tempCondition2 = new IntBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
        condition = tempCondition2;    
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }

    private void AddIntCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        int newVariable1 = 0;
        int newVariable2 = 0;
        this.tempIntBasedCondition = new IntBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        BSconnection.IntBasedConditions.Add(tempIntBasedCondition);       
    }

    private void ModifyFloatCondition(FloatBasedCondition condition)
    {
        FloatBasedCondition tempCondition = condition;
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        string newConditionName = tempCondition.conditionName;
        Operation newConditionOperation = tempCondition.operation;
        float newVariable1 = tempCondition.variable1;
        float newVariable2 = tempCondition.variable2;
        newConditionName = EditorGUILayout.TextField(newConditionName);
            
        newVariable2 = EditorGUILayout.FloatField(newVariable2);
        EditorGUIUtility.labelWidth = 35;
        newConditionOperation = (Operation)EditorGUILayout.EnumPopup("FLOAT" ,newConditionOperation, GUILayout.ExpandWidth(false));    
        EditorGUIUtility.labelWidth = 0;       
        FloatBasedCondition tempCondition2 = new FloatBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
        condition = tempCondition2;    
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }

    private void AddFloatCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        float newVariable1 = 0.0f;
        float newVariable2 = 0.0f;
        this.tempFloatBasedCondition = new FloatBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        BSconnection.FloatBasedConditions.Add(tempFloatBasedCondition);       
    }

    private void ModifyBoolCondition(BoolBasedCondition condition)
    {
        BoolBasedCondition tempCondition = condition;
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        string newConditionName = tempCondition.conditionName;
        Operation newConditionOperation = tempCondition.operation;
        bool newVariable1 = tempCondition.variable1;
        bool newVariable2 = tempCondition.variable2;
        newConditionName = EditorGUILayout.TextField(newConditionName);
            
        newVariable2 = EditorGUILayout.Toggle(newVariable2);
        EditorGUIUtility.labelWidth = 35;
        newConditionOperation = (Operation)EditorGUILayout.EnumPopup("BOOL" ,newConditionOperation, GUILayout.ExpandWidth(false));    
        EditorGUIUtility.labelWidth = 0;       
        BoolBasedCondition tempCondition2 = new BoolBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
        condition = tempCondition2;    
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }

    private void AddBoolCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        bool newVariable1 = false;
        bool newVariable2 = true;
        this.tempBoolBasedCondition = new BoolBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        BSconnection.BoolBasedConditions.Add(tempBoolBasedCondition);       
    }

    private void ModifyStringCondition(StringBasedCondition condition)
    {
        StringBasedCondition tempCondition = condition;
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        string newConditionName = tempCondition.conditionName;
        Operation newConditionOperation = tempCondition.operation;
        string newVariable1 = tempCondition.variable1;
        string newVariable2 = tempCondition.variable2;
        newConditionName = EditorGUILayout.TextField(newConditionName);
            
        newVariable2 = EditorGUILayout.TextField(newVariable2);
        EditorGUIUtility.labelWidth = 35;
        newConditionOperation = (Operation)EditorGUILayout.EnumPopup("STRING" ,newConditionOperation, GUILayout.ExpandWidth(false));    
        EditorGUIUtility.labelWidth = 0;       
        StringBasedCondition tempCondition2 = new StringBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
        condition = tempCondition2;    
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }

    private void AddStringCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        string newVariable1 = "";
        string newVariable2 = "";
        this.tempStringBasedCondition = new StringBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        BSconnection.StringBasedConditions.Add(tempStringBasedCondition);       
    }
    

}
