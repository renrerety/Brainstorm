using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] LevelData scene;
    [SerializeField] private Slider loadingProgress;
    [SerializeField] private float loadingSpeed;

    private float progressValue;
    public void LoadGame()
    {
        if(SkinSelector.instance.skins[SkinSelector.instance.index].unlocked)
        {
            loadingProgress.gameObject.SetActive(true);
            PlayerData.instance.currentSkin = SkinSelector.instance.skins[SkinSelector.instance.index].type;
            StartCoroutine(LoadSyncAsync());
        }
    }

    private float t;
    private AsyncOperation operation;
    IEnumerator LoadSyncAsync()
    {
        t = 0;
        loadingProgress.value = 0;
        
        operation = SceneManager.LoadSceneAsync(scene.name,LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.progress <= 1)
        {
            t += Time.deltaTime;
            loadingProgress.value = Mathf.Lerp(0, 1, t);
            
            if (loadingProgress.value >= 1)
            {
                operation.allowSceneActivation = true;
                yield return new WaitForEndOfFrame();
                loadingProgress.gameObject.SetActive(false);
                GameObject.Find("Music").GetComponent<AudioSource>().Stop();
                loadingProgress.value = 0;
            }
            yield return null;
        }
    }
}
