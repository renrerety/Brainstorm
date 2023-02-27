using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    //Script come from class exercice
    public string sceneName;

#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneAsset;
    public UnityEditor.SceneAsset Scene
    {
        get => sceneAsset;
        set
        {
            sceneAsset = value;
            sceneName = sceneAsset.name;
        }
    }
#endif
}
