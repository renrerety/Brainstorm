using System;
using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(LoadSyncAsync());
        loadingProgress.gameObject.SetActive(true);
        MenuManager.instance.ToggleMainMenu();
    }

    private float t;
    IEnumerator LoadSyncAsync()
    {
        t = 0;
        loadingProgress.value = 0;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.name);
        operation.allowSceneActivation = false;
        
        
        while (loadingProgress.value != 1)
        {
            progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            
            t += Time.deltaTime;
            loadingProgress.value = Mathf.Lerp(0, 1, t);
            
            if (loadingProgress.value >= 1)
            {
                operation.allowSceneActivation = true;
                yield return new WaitForEndOfFrame();
                loadingProgress.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
