using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] Button back_button;
    [SerializeField] GameObject popupMenu;
    [SerializeField] GameObject mainMenu;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Title")
        {
            TogglePopupMenu();
        }
    }
    public void TogglePopupMenu()
    {
        if (popupMenu.activeSelf)
        {
            popupMenu.SetActive(false);
        }
        else
        {
            popupMenu.SetActive(true);
            if (SceneManager.GetActiveScene().name != "Title")
            {
                back_button.gameObject.SetActive(true);
            }
            else
            {
                back_button.gameObject.SetActive(false);
            }
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
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Title");
        ToggleMainMenu();
        TogglePopupMenu();
    }
    public void Exit()
    {
        Application.Quit();
    }
}