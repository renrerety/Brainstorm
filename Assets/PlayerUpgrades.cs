using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public static PlayerUpgrades instance;
    
    private int hpUp;
    private int damageUp;
    private int speedUp;
    private int xpUp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateUpgrades(int hpUp, int damageUp,int speedUp,int xpUp)
    {
        this.hpUp = hpUp;
        this.damageUp = damageUp;
        this.speedUp = speedUp;
        this.xpUp = xpUp;
        
        LoadPlayerData.instance.UpdateDisplay();
    }
}
