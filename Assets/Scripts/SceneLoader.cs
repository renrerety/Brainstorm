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
            DontDestroyOnLoad(this);
        }
    }

    [SerializeField] private LevelData scene;
    private string sceneName;

    public void LoadAssetsWithLabels(string label)
    {
        Addressables
            .LoadAssetsAsync<UnityEngine.Object>(label, x => { }, Addressables.MergeMode.Union);
    }
    public void LoadScene(LevelData scene,bool additive, List<string> labels)
    {
        this.sceneName = scene.name;
        if (!additive)
        {
            Addressables
                .LoadAssetsAsync<UnityEngine.Object>(labels, x => { }, Addressables.MergeMode.Union)
                .Completed += SceneLoader_Completed_Single;
        }
        else
        {
            Addressables
                .LoadAssetsAsync<UnityEngine.Object>(labels, x => { }, Addressables.MergeMode.Union)
                .Completed += SceneLoader_Completed_Additive;
        }
    }

    public void LoadGameScene(string scene)
    {
        this.sceneName = scene;
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new object[] { "Systems" }, null,
            Addressables.MergeMode.Union).Completed += SceneLoader_Completed_Additive;
    }
    void SceneLoader_Completed_Single(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
    void SceneLoader_Completed_Additive(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        Addressables.LoadSceneAsync("GameSystems", LoadSceneMode.Additive);
    }
}
