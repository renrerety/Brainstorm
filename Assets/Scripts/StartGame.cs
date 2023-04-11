using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class StartGame : MonoBehaviour
{
    [SerializeField] private LevelData gameSystems;
    [SerializeField] private GameObject mapSelection;
    
    [SerializeField] public LevelData scene;
    [SerializeField] private Slider loadingProgress;
    [SerializeField] private float loadingSpeed;

    private float progressValue;
    public void LoadGame()
    {
        if(MapSelector.instance.mapList[MapSelector.instance.index].unlocked)
        {
            mapSelection.gameObject.SetActive(false);
            scene = MapSelector.instance.mapList[MapSelector.instance.index].level;
            loadingProgress.gameObject.SetActive(true);
            PlayerData.instance.currentSkin = SkinSelector.instance.skins[SkinSelector.instance.index].type;
            StartCoroutine(LoadSyncAsync());
        }
    }

    private float t;
    IEnumerator LoadSyncAsync()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        SceneLoader.instance.LoadScene(scene, false , new List<string> {MapSelector.instance.mapList[MapSelector.instance.index].label.ToString()});
        
        t = 0;
        loadingProgress.value = 0;
        
        while (loadingProgress.value <= 1)
        {
            t += Time.deltaTime;
            loadingProgress.value = Mathf.Lerp(0, 1, t);

            if (loadingProgress.value >= 1)
            {
                yield return new WaitForEndOfFrame();
                loadingProgress.gameObject.SetActive(false);
                
                loadingProgress.value = 0;
                //operation.Task.Result.ActivateAsync();
            }
            yield break;
        }
    }
}
