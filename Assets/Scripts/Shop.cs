using System;
using System.Collections;
using System.Collections.Generic;
using Localization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int hpUp = 0;
    private int damageUp = 0;
    private int speedUp = 0;
    private int xpUp = 0;

    private int hpPrice = 500;
    private int damagePrice = 500;
    private int speedPrice = 500;
    private int xpPrice = 500;
    
    [SerializeField] private Text hpTxt;
    [SerializeField] private Text damageTxt;
    [SerializeField] private Text speedTxt;
    [SerializeField] private Text xpTxt;

    [SerializeField] private Text hpPriceTxt;
    [SerializeField] private Text damagePriceTxt;
    [SerializeField] private Text speedPriceTxt;
    [SerializeField] private Text xpPriceTxt;
    

    public void AddUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "health":
                if (PlayerData.instance.persistentData.gold >= hpPrice)
                {
                    if (hpUp < 5)
                    {
                        PlayerData.instance.persistentData.gold -= hpPrice;
                        hpUp = Mathf.Clamp(++hpUp, 0, 5);
                        hpPrice *= 2;
                        if (hpUp == 5)
                        {
                            HideParent(hpPriceTxt.gameObject);
                        }
                    }
                }
                break;
            case "damage":
                if (PlayerData.instance.persistentData.gold >= damagePrice)
                {
                    if (damageUp < 5)
                    {
                        PlayerData.instance.persistentData.gold -= damagePrice;
                        damageUp = Mathf.Clamp(++damageUp, 0, 5);
                        damagePrice *= 2;
                        if (damageUp == 5)
                        {
                            HideParent(damagePriceTxt.gameObject);
                        }
                        
                    }
                }
                break;
            case "speed":
                if (PlayerData.instance.persistentData.gold > speedPrice)
                {
                    if (speedUp < 5)
                    {
                        PlayerData.instance.persistentData.gold -= speedPrice;
                        speedUp = Mathf.Clamp(++speedUp, 0, 5);
                        speedPrice *= 2;
                        if (speedUp == 5)
                        {
                            HideParent(speedPriceTxt.gameObject);
                        }
                        
                    }
                }
                break;
            case "xp":
                if (PlayerData.instance.persistentData.gold > xpPrice)
                {
                    if (xpUp < 5)
                    {
                        PlayerData.instance.persistentData.gold -= xpPrice;
                        xpUp = Mathf.Clamp(++xpUp, 0, 5);
                        xpPrice *= 2;
                        if (xpUp == 5)
                        {
                            HideParent(xpPriceTxt.gameObject);
                        }
                        
                    }
                }
                break;
        }
        LoadDisplay();
    }

    public void SubtractUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "health":
                if (hpUp > 0)
                {
                    hpPriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    hpUp = Mathf.Clamp(--hpUp, 0, 5);
                    hpPrice /= 2;
                    PlayerData.instance.persistentData.gold += hpPrice;
                }
                break;
            case "damage":
                if (damageUp > 0)
                {
                    damagePriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    damageUp = Mathf.Clamp(--damageUp, 0, 5);
                    damagePrice /= 2;
                    PlayerData.instance.persistentData.gold += damagePrice;
                }
                break;
            case "speed":
                if (speedUp > 0)
                {
                    speedPriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    speedUp = Mathf.Clamp(--speedUp, 0, 5);
                    speedPrice /= 2;
                    PlayerData.instance.persistentData.gold += speedPrice;
                }
                break;
            case "xp":
                if (xpUp > 0)
                {
                    xpPriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    xpUp = Mathf.Clamp(--xpUp, 0, 5);
                    xpPrice /= 2;
                    PlayerData.instance.persistentData.gold += xpPrice;
                }
                break;
        }

        LoadDisplay();
    }

    public void LoadDisplay()
    {
        StartCoroutine(UpdateDisplay());
    }

    public IEnumerator UpdateDisplay()
    {
        UpdatePrices();
        UpdateUpgrades();
        
        hpTxt.GetComponent<TextTranslator>().Translate();
        damageTxt.GetComponent<TextTranslator>().Translate();
        speedTxt.GetComponent<TextTranslator>().Translate();
        xpTxt.GetComponent<TextTranslator>().Translate();

        yield return new WaitForEndOfFrame();
        
        hpTxt.text += hpUp+"/5";
        damageTxt.text += damageUp+"/5";
        speedTxt.text += speedUp+"/5";
        xpTxt.text += xpUp+"/5";
        
        LoadPlayerData.instance.UpdateDisplay();
    }

    public void UpdatePrices()
    {
        hpPriceTxt.text = hpPrice.ToString();
        damagePriceTxt.text = damagePrice.ToString();
        speedPriceTxt.text = speedPrice.ToString();
        xpPriceTxt.text = xpPrice.ToString();
    }

    private void UpdateUpgrades()
    {
        PlayerUpgrades upgrades =  PlayerData.instance.persistentData.upgrades;
        upgrades.hpUp = hpUp;
        upgrades.damageUp = damageUp;
        upgrades.speedUp = speedUp;
        upgrades.xpUp = xpUp;
        
        LoadPlayerData.instance.UpdateDisplay();
    }

    public void Save()
    {
        BinarySaveFormatter.Serialize();

        StartCoroutine(BinarySaveFormatter.UploadToDb());
    }

    public void LoadValue()
    {
        PlayerUpgrades upgrades =  PlayerData.instance.persistentData.upgrades;
        
        hpUp = upgrades.hpUp;
        hpPrice *= hpUp;
        if (hpUp == 5)
        {
            HideParent(hpPriceTxt.gameObject);
        }

        if (hpUp == 0)
        {
            hpPrice = 500;
        }

        damageUp = upgrades.damageUp;
        damagePrice *= hpUp;
        if (damageUp == 5)
        {
            HideParent(damagePriceTxt.gameObject);
        }
        if (damageUp == 0)
        {
            damagePrice = 500;
        }
        
        speedUp = upgrades.speedUp;
        speedPrice *= speedUp;
        if (speedUp == 5)
        {
            HideParent(speedPriceTxt.gameObject);
        }
        if (speedUp == 0)
        {
            speedPrice = 500;
        }
        
        xpUp = upgrades.xpUp;
        xpPrice *= xpUp;
        if (xpUp == 5)
        {
            HideParent(xpPriceTxt.gameObject);
        }
        if (xpUp == 0)
        {
            xpPrice = 500;
        }

    }

    public void HideParent(GameObject obj)
    {
        obj.transform.parent.gameObject.SetActive(false);
    }
}

