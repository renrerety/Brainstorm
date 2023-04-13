using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public static PlayerLevel instance;
    public float xp;
    public float requiredXP;

    public int level = 1;

    [SerializeField] private Slider xpSlider;
    [SerializeField] private Text levelText;
    [SerializeField] private LevelUpPanel levelUpPanel;
    [SerializeField] private AudioClip xpClip;

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
    public void AddXp(float xpToAdd)
    {
        xpToAdd += (PlayerData.instance.persistentData.upgrades.xpUp / 2f);
        xp += xpToAdd;
        GetComponent<AudioSource>().PlayOneShot(xpClip);
        
        CheckLevelUp();
        UpdateXpSlider();
    }
    private void UpdateXpSlider()
    {
        xpSlider.value = (xp / requiredXP) * 100;
    }

    private void CalculateRequiredXp()
    {
        requiredXP = (float)Math.Pow((level / 0.2f),1.5f);
    }

    private void CheckLevelUp()
    {
        if (xp >= requiredXP)
        {
            level++;
            xp = 0;
            CalculateRequiredXp();
            levelUpPanel.gameObject.SetActive(true);
            levelUpPanel.RollWeapons();
            Time.timeScale = 0;
        }

        levelText.text = level.ToString();
    }
    private void Start()
    {
        CalculateRequiredXp();
    }
}
