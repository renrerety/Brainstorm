using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerData : MonoBehaviour
{
    public static LoadPlayerData instance;
    [SerializeField] private Text goldText;

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
    // Start is called before the first frame update
    void Start()
    {
        if (!Signup.instance.firstLogin)
        {
            BinarySaveFormatter.Deserialize();
        }
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        goldText.text = PlayerData.instance.persistentData.gold.ToString();
    }
}
