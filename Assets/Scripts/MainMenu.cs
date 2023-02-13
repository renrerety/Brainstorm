using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] LevelData scene;
    public void LoadGame()
    {
        SceneManager.LoadScene(scene.name);
        MenuManager.instance.ToggleMainMenu();
    }
}
