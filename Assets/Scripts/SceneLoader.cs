using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private LevelData scene;
    private string sceneName;

    public void LoadAssetsWithLabels(string label)
    {
        Addressables
            .LoadAssetsAsync<UnityEngine.Object>(label, x => { }, Addressables.MergeMode.Union);
    }
    public void LoadScene(LevelData scene, List<string> labels)
    {
        this.sceneName = scene.name;
        Addressables
            .LoadAssetsAsync<UnityEngine.Object>(labels, x => { }, Addressables.MergeMode.Union)
            .Completed += SceneLoader_Completed;
    }
    void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
