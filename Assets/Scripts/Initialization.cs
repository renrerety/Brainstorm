using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private LevelData scene;
    private void LoadScene()
    {
        Addressables
            .LoadAssetsAsync<UnityEngine.Object>(new List<string>() { "Menu" }, x => { }, Addressables.MergeMode.None)
            .Completed += SceneLoader_Completed;
    }
    void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        Addressables.LoadSceneAsync(scene.name, LoadSceneMode.Single);
    }

    private void Start()
    {
        LoadScene();
    }
}
