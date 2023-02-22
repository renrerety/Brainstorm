using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool gameOver;
    [SerializeField] private LevelData mainMenu;

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenu.name);
        Time.timeScale = 1;
    }
}
