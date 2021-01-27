using UnityEngine;
using UnityEditor;
public class EditorWindowExample : EditorWindow
{
    /* Variable: trait
     * Holds a Trait that is later saved
     */
    static Trait trait;

    void OnEnable()
    {
        InitData();
    }

    public static void InitData()
    {
        trait = (Trait)ScriptableObject.CreateInstance(typeof(Trait));
    }

    [MenuItem("Window/Trait Editor")]
    public static void OpenWindow()
    {
        EditorWindowExample window = (EditorWindowExample)GetWindow(typeof(EditorWindowExample));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Trait name");
        //Edit trait name
        trait.traitName = EditorGUILayout.TextField(trait.traitName);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save trait"))
        {
            SaveTraitData();
        }
    }

    /* Function: SaveTraitData
     * Handles saving a Trait with .asset extention
     */
    void SaveTraitData()
    {
        string traitPath = "Assets/Prefabs/traits/" + trait.traitName;
        // create the .asset file
        Trait newTrait = (Trait)ScriptableObject.CreateInstance(typeof(Trait));
        newTrait.traitName = trait.traitName;
        AssetDatabase.CreateAsset(newTrait, traitPath + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}