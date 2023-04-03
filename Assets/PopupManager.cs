using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
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
    public void BackToMainMenu()
    {
        confirmationPrompt.SetActive(false);
        SceneManager.LoadScene("Title");
        GameObject musicObj = GameObject.Instantiate(music);
        musicObj.gameObject.name = "Music";
        Time.timeScale = 1;
    }
}
