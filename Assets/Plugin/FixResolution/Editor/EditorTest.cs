using UnityEngine;
using UnityEditor;

public class EditorWindowSample : EditorWindow
{
    [MenuItem("Tools/SomeToolWindow")] // MenuItem() の中が, Unity Editor 上部ツールバーから開くための場所になります.
    private static void Open()
    {
        GetWindow<EditorWindowSample>("タブに表示したいタイトル");
    }

    [MenuItem("GameObject/UI/FixResolution/Canvas")]
    public static void Create()
    {
        string name = "target";
        string prefabPath = "Assets/Plugin/FixResolution/Prefab/";
        
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}FixCanvas.prefab");

        GameObject g = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Undo.RegisterCreatedObjectUndo(g, "Create New GameObject");
    }
}