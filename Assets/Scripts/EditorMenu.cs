#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EditorMenu : MonoBehaviour
{
    //Script come from class exercise
    static List<LevelData> scenesSO = new List<LevelData>();
    [MenuItem("Tools/Count Scene Number")]
    static void CountSceneNumber()
    {
        Debug.Log(GetSceneAssets().Count);
    }

    static string[] searchInFolders = new[] { "Assets/Scenes/" }; 
    static List<SceneAsset> GetSceneAssets() { 
        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneAsset", searchInFolders); 
        var sceneAssets = new List<SceneAsset>(); 
        foreach (var sceneGuid in sceneGuids) 
        { 
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid); 
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath)); 
        } 
        return sceneAssets; 
    }
    [MenuItem("Tools/Update Scene Assets")]
    static void UpdateSceneAssets()
    {
        if (!AssetDatabase.IsValidFolder(Path.Combine("Assets", "ScriptableObjects","ScenesSO")))
        {
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
            AssetDatabase.CreateFolder(Path.Combine("Assets", "ScriptableObjects"), "ScenesSO");
        }
        foreach(var sceneAsset in GetSceneAssets())
        {
            var sceneData = ScriptableObject.CreateInstance<LevelData>();
            sceneData.Scene = sceneAsset;
            string assetPath = Path.Combine("Assets", "ScriptableObjects","ScenesSO", sceneData.sceneName + ".asset");
            AssetDatabase.CreateAsset(sceneData, assetPath);
            scenesSO.Add(sceneData);
        }
    }
    [MenuItem("Tools/Refresh Scene Assets")]
    static void RefreshSceneAssets()
    {
        foreach(LevelData sceneAsset in scenesSO)
        {
            if(sceneAsset.sceneName != sceneAsset.Scene.name)
            {
                sceneAsset.sceneName = sceneAsset.Scene.name;
                sceneAsset.name = sceneAsset.Scene.name;
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(sceneAsset), sceneAsset.sceneName +".asset");
            }
        }
    }
}
#endif
