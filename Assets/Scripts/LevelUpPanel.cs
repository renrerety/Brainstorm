using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    public static LevelUpPanel Instance;

    [SerializeField] public List<WeaponMaster> weaponRefs = new List<WeaponMaster>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
