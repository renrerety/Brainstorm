using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int hpUp;
    private int damageUp;
    private int speedUp;
    private int xpUp;

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
                        hpPriceTxt.text = hpPrice.ToString();
                        if (hpUp == 5)
                        {
                            hpPriceTxt.gameObject.transform.parent.gameObject.SetActive(false);
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
                        damagePriceTxt.text = damagePrice.ToString();
                        if (damageUp == 5)
                        {
                            damagePriceTxt.gameObject.transform.parent.gameObject.SetActive(false);
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
                        speedPriceTxt.text = speedPrice.ToString();
                        if (speedUp == 5)
                        {
                            speedPriceTxt.gameObject.transform.parent.gameObject.SetActive(false);
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
                        xpPriceTxt.text = xpPrice.ToString();
                        if (xpUp == 5)
                        {
                            xpPriceTxt.gameObject.transform.parent.gameObject.SetActive(false);
                        }
                        
                    }
                }
                break;
        }
        PlayerUpgrades.instance.UpdateUpgrades(hpUp,damageUp,speedUp,xpUp);
        UpdateDisplay();
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
                    hpPriceTxt.text = hpPrice.ToString();
                }
                break;
            case "damage":
                if (damageUp > 0)
                {
                    damagePriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    damageUp = Mathf.Clamp(--damageUp, 0, 5);
                    damagePrice /= 2;
                    PlayerData.instance.persistentData.gold += damagePrice;
                    damagePriceTxt.text = damagePrice.ToString();
                }
                break;
            case "speed":
                if (speedUp > 0)
                {
                    speedPriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    speedUp = Mathf.Clamp(--speedUp, 0, 5);
                    speedPrice /= 2;
                    PlayerData.instance.persistentData.gold += speedPrice;
                    speedPriceTxt.text = speedPrice.ToString();
                }
                break;
            case "xp":
                if (xpUp > 0)
                {
                    xpPriceTxt.gameObject.transform.parent.gameObject.SetActive(true);
                    xpUp = Mathf.Clamp(--xpUp, 0, 5);
                    xpPrice /= 2;
                    PlayerData.instance.persistentData.gold += xpPrice;
                    xpPriceTxt.text = xpPrice.ToString();
                }
                break;
        }
        PlayerUpgrades.instance.UpdateUpgrades(hpUp,damageUp,speedUp,xpUp);
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        hpTxt.text = Localization.Localization.instance.GetString("hpUp")+hpUp+"/5";
        damageTxt.text = Localization.Localization.instance.GetString("damageUp")+damageUp+"/5";
        speedTxt.text = Localization.Localization.instance.GetString("speedUp")+speedUp+"/5";
        xpTxt.text = Localization.Localization.instance.GetString("xpUp")+xpUp+"/5";
    }
}
