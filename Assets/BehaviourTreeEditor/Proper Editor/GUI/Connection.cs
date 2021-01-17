using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[Serializable]
public class Connection
{
    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
    public Action<Connection> OnClickRemoveConnection;
    TypeOfCondition option;
    public List<IntBasedCondition> intBasedConditions;

    public List<string> intBasedConditionsToString;
    public List<FloatBasedCondition> floatBasedConditions; 
    public List<string> floatBasedConditionsToString;
    public List<BoolBasedCondition> boolBasedConditions;
    public List<string> boolBasedConditionsToString;
    public List<StringBasedCondition> stringBasedConditions;
    public List<string> stringBasedConditionsToString;
    public IntBasedCondition tempIntBasedCondition;
    public FloatBasedCondition tempFloatBasedCondition;
    public BoolBasedCondition tempBoolBasedCondition;
    public StringBasedCondition tempStringBasedCondition;
    
    public string previousNodeID;
    public string nextNodeID;
    Vector2 scrollView;
    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        this.OnClickRemoveConnection = OnClickRemoveConnection;
        this.intBasedConditions = new List<IntBasedCondition>();
        this.floatBasedConditions = new List<FloatBasedCondition>();
        this.boolBasedConditions = new List<BoolBasedCondition>();
        this.stringBasedConditions = new List<StringBasedCondition>();
        this.intBasedConditionsToString = new List<string>();
        this.floatBasedConditionsToString = new List<string>();
        this.boolBasedConditionsToString = new List<string>();
        this.stringBasedConditionsToString = new List<string>();
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
        
        ModifyIntConditions();
        ModifyFloatConditions();
        ModifyBoolConditions();
        ModifyStringConditions();

        
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

    private void ModifyIntConditions()
    {
        for (int i = 0; i < this.intBasedConditions.Count; i++)
        {
            IntBasedCondition tempCondition = this.intBasedConditions[i];
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
            this.intBasedConditions[i] = new IntBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

    }

    private void AddIntCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        int newVariable1 = 0;
        int newVariable2 = 0;
        this.tempIntBasedCondition = new IntBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.intBasedConditions.Add(tempIntBasedCondition);       
    }

    private void ModifyFloatConditions()
    {

        for (int i = 0; i < this.floatBasedConditions.Count; i++)
        {
            FloatBasedCondition tempCondition = this.floatBasedConditions[i];
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
            this.floatBasedConditions[i] = new FloatBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }      
    }

    private void AddFloatCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        float newVariable1 = 0.0f;
        float newVariable2 = 0.0f;
        this.tempFloatBasedCondition = new FloatBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.floatBasedConditions.Add(tempFloatBasedCondition);       
    }

    private void ModifyBoolConditions()
    {

        for (int i = 0; i < this.boolBasedConditions.Count; i++)
        {
            BoolBasedCondition tempCondition = this.boolBasedConditions[i];
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
            this.boolBasedConditions[i] = new BoolBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);    
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        

    }

    private void AddBoolCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        bool newVariable1 = false;
        bool newVariable2 = true;
        this.tempBoolBasedCondition = new BoolBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.boolBasedConditions.Add(tempBoolBasedCondition);       
    }

    private void ModifyStringConditions()
    {
        for (int i = 0; i < this.stringBasedConditions.Count; i++)
        {
            StringBasedCondition tempCondition = this.stringBasedConditions[i];
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
            this.stringBasedConditions[i] = new StringBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2);  
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }       

    }

    private void AddStringCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        string newVariable1 = "";
        string newVariable2 = "";
        this.tempStringBasedCondition = new StringBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.stringBasedConditions.Add(tempStringBasedCondition);       
    }

    public void ConvertAllConditionsToString()
    {
        foreach(IntBasedCondition condition in intBasedConditions)
        {
            this.intBasedConditionsToString.Add(condition.ToString());
        }
        foreach(FloatBasedCondition condition in floatBasedConditions)
        {
            this.floatBasedConditionsToString.Add(condition.ToString());
        }
        foreach(BoolBasedCondition condition in boolBasedConditions)
        {
            this.boolBasedConditionsToString.Add(condition.ToString());
        }
        foreach(StringBasedCondition condition in stringBasedConditions)
        {
            this.stringBasedConditionsToString.Add(condition.ToString());
        }
    }
    

}
