using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] Button back_button;
    [SerializeField] GameObject popupMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject confirmationPrompt;
    [SerializeField] Button backToMenu_btn;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
        }
    }
    public void ToggleMainMenu()
    {
        if (mainMenu.activeSelf)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    public void ToggleOptionMenu()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);
        }
        else
        {
            optionMenu.SetActive(true);
        }
    }

    public void ToggleConfirmation()
    {
        if (confirmationPrompt.activeSelf)
        {
            confirmationPrompt.SetActive(false);
        }
        else
        {
            confirmationPrompt.SetActive(true);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
