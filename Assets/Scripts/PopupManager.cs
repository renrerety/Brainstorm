using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private LevelData scene;
    
    public static PopupManager instance;
    [SerializeField] private GameObject popupMenu;

    [SerializeField] private GameObject confirmationPrompt;

    [SerializeField] private GameObject music;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Title" && !confirmationPrompt.activeSelf)
        {
            TogglePopupMenu();
        }
    }
    public void TogglePopupMenu()
    {
        GameOver gameOver = FindObjectOfType<GameOver>();
        if (gameOver == null || !gameOver.gameOver)
        {
            if (popupMenu.activeSelf)
            {
                popupMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                popupMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    private IEnumerator BackToMainMenu()
    {
        confirmationPrompt.SetActive(false);
        SceneLoader.instance.LoadScene(scene,false,new List<string>{"Menu"});
        yield return new WaitForEndOfFrame();
        GameObject.Find("Music").GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(BackToMainMenu());
    }
}
