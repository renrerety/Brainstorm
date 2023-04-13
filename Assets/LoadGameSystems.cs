using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadGameSystems : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Addressables
                    .LoadAssetsAsync<UnityEngine.Object>(new List<string>() { "Systems" }, x => { }, Addressables.MergeMode.Union)
                    .Completed += LoadGameSystemsScene;
        WorldScrolling.instance.player = GameObject.Find("Player").transform;
    }

    public void LoadGameSystemsScene(AsyncOperationHandle<IList<UnityEngine.Object> > op)
    {
        //Addressables.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
    }

    private void SetupPlayerHealth()
    {
    }
}
