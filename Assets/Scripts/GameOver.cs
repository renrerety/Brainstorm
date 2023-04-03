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
        Time.timeScale = 1;
        PopupManager.instance.ReturnToMainMenu();
    }
}
