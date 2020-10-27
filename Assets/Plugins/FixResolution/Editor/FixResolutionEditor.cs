using UnityEditor;
using UnityEngine;

namespace Plugin.FixResolution.Editor
{
    public class FixResolutionEditor : EditorWindow
    {
        static string prefabPath = "Assets/Plugins/FixResolution/Prefab/";
        
        /// <summary>
        /// 右クリックから解像度対応用のキャンバスを作成するためのエディタ拡張
        /// </summary>
        [MenuItem("GameObject/UI/FixResolution/Canvas")]
        public static void Create()
        {
            //アセットをロード
            GameObject prefabCanvas = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}FixCanvas.prefab");
            GameObject prefabCamera = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}CanvasCamera.prefab");
            // GameObject prefabEventSystem = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}EventSystem.prefab");

            //ロードしたアセットを配置する
            GameObject canvas = PrefabUtility.InstantiatePrefab(prefabCanvas) as GameObject;
            GameObject camera = PrefabUtility.InstantiatePrefab(prefabCamera) as GameObject;
            // GameObject eventSystem = PrefabUtility.InstantiatePrefab(prefabEventSystem) as GameObject;
            canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
            
            //Undo対応
            Undo.RegisterCreatedObjectUndo(canvas, "Create New GameObject");
            Undo.RegisterCreatedObjectUndo(camera, "Create New GameObject");
            // Undo.RegisterCreatedObjectUndo(eventSystem, "Create New GameObject");
            
            //Prefabのリンクを解除する
            PrefabUtility.UnpackPrefabInstance(canvas,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
            PrefabUtility.UnpackPrefabInstance(camera,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
            // PrefabUtility.UnpackPrefabInstance(eventSystem,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
        }
    }
}