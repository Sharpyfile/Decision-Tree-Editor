using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

[Serializable]
public class Connection
{
    /* Variable: inPoint
    * ConnectionPoint that leads to the next Node
    */
    public ConnectionPoint inPoint;

    /* Variable: outPoint
    * ConnectionPoint that leads to the previous Node
    */
    public ConnectionPoint outPoint;

    /* Variable: OnClickRemoveConnection
    * Action that is executed after button is pressed
    * Removes the condition
    */
    public Action<Connection> OnClickRemoveConnection;

    /* Variable: option
    * Enum TypeOfCondition used for defining type for new condition 
    */
    private TypeOfCondition option;

    /* Variable: intBasedConditions
     * List of the condition with type int
     */
    public List<IntBasedCondition> intBasedConditions;

    /* Variable: intBasedConditionsToString
     * List of the condition with type int converted to one string
     */
    public List<string> intBasedConditionsToString;

    /* Variable: floatBasedConditions
     * List of the condition with type float
     */
    public List<FloatBasedCondition> floatBasedConditions;

    /* Variable: floatBasedConditionsToString
     * List of the condition with type float converted to one string
     */
    public List<string> floatBasedConditionsToString;

    /* Variable: boolBasedConditions
     * List of the condition with type bool
     */
    public List<BoolBasedCondition> boolBasedConditions;

    /* Variable: boolBasedConditionsToString
     * List of the condition with type bool converted to one string
     */
    public List<string> boolBasedConditionsToString;

    /* Variable: stringBasedConditions
     * List of the condition with type string
     */
    public List<StringBasedCondition> stringBasedConditions;

    /* Variable: stringBasedConditionsToString
     * List of the condition with type string converted to one string
     */
    public List<string> stringBasedConditionsToString;

    /* Variable: tempIntBasedCondition
     * Temporary condition used when adding a new condition
     */
    public IntBasedCondition tempIntBasedCondition;

    /* Variable: tempFloatBasedCondition
     * Temporary condition used when adding a new condition
     */
    private FloatBasedCondition tempFloatBasedCondition;

    /* Variable: tempBoolBasedCondition
     * Temporary condition used when adding a new condition
     */
    private BoolBasedCondition tempBoolBasedCondition;

    /* Variable: tempStringBasedCondition
     * Temporary condition used when adding a new condition
     */
    private StringBasedCondition tempStringBasedCondition;

    /* Variable: previousNodeID
     * Represents ID of the node that leads to this connection
     */
    public string previousNodeID;

    /* Variable: nextNodeID
     * Represents ID of the node that will be next after all conditions in this
     * connection will be met
     */
    public string nextNodeID;

    /* Variable: scrollView
     * Used for scrolling through conditions
     */
    private Vector2 scrollView;

    /* Variable: connectionTrait
     * Trait for this connection
     */
    public Trait connectionTrait;

    /* Function: Connection
     * Constructor, takes two ConnectionPoint and Action<Connection> for handling removing connection
     */
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
        this.connectionTrait = null;
    }
#if UNITY_EDITOR

    /* Function: Draw
     * Draws Condition  connection between inPoint and outPoint, handles adding and modifying
     * different conditions
     */
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
        connectionTrait = EditorGUILayout.ObjectField(connectionTrait, typeof(ScriptableObject), true) as Trait;
        
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

    /* Function: ModifyIntConditions
     * Draws GUI responsible for modifying Condition with type int 
     */
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

    /* Function: AddIntCondition
     * Adds new Condition with type int
     */
    private void AddIntCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        int newVariable1 = 0;
        int newVariable2 = 0;
        this.tempIntBasedCondition = new IntBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.intBasedConditions.Add(tempIntBasedCondition);       
    }

    /* Function: ModifyFloatConditions
     * Draws GUI responsible for modifying Condition with type float 
     */
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

    /* Function: AddFloatCondition
     * Adds new Condition with type float
     */
    private void AddFloatCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        float newVariable1 = 0.0f;
        float newVariable2 = 0.0f;
        this.tempFloatBasedCondition = new FloatBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.floatBasedConditions.Add(tempFloatBasedCondition);       
    }

    /* Function: ModifyBoolConditions
     * Draws GUI responsible for modifying Condition with type bool 
     */
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
    /* Function: AddBoolCondition
     * Adds new Condition with type bool
     */
    private void AddBoolCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        bool newVariable1 = false;
        bool newVariable2 = true;
        this.tempBoolBasedCondition = new BoolBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.boolBasedConditions.Add(tempBoolBasedCondition);       
    }

    /* Function: ModifyStringConditions
     * Draws GUI responsible for modifying Condition with type string 
     */
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

    /* Function: AddStringCondition
     * Adds new Condition with type string
     */
    private void AddStringCondition()
    {
        string newConditionName = "New condition";
        Operation newConditionOperation = Operation.IsEqual;
        string newVariable1 = "";
        string newVariable2 = "";
        this.tempStringBasedCondition = new StringBasedCondition(newConditionName, newConditionOperation, newVariable1, newVariable2); 
        this.stringBasedConditions.Add(tempStringBasedCondition);       
    }

#endif

    /* Function: ConvertAllConditionsToString
     * Converts all conditions in Connection and adds them to specific List<string>
     * 
     */
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

    /* Function: ConvertAllConditionsToString
     * Converts all conditions in Connection and adds them to connectionContainer
     * 
     */
    public void ConvertAllConditionsToString(ConnectionContainer connectionContainer)
    {
        foreach(IntBasedCondition condition in intBasedConditions)
        {
            connectionContainer.intBasedConditions.Add(condition.ToString());
        }
        foreach(FloatBasedCondition condition in floatBasedConditions)
        {
            connectionContainer.floatBasedConditions.Add(condition.ToString());
        }
        foreach(BoolBasedCondition condition in boolBasedConditions)
        {
            connectionContainer.boolBasedConditions.Add(condition.ToString());
        }
        foreach(StringBasedCondition condition in stringBasedConditions)
        {
            connectionContainer.stringBasedConditions.Add(condition.ToString());
        }
    }
    

}
