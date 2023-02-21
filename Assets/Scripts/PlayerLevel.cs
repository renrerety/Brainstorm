using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public float xp;
    public float requiredXP;

    public int level = 1;

    [SerializeField] private Slider xpSlider;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private AudioClip xpClip;
    
    public void AddXp(float xpToAdd)
    {
        xp += xpToAdd;
        GetComponent<AudioSource>().PlayOneShot(xpClip);
        
        CheckLevelUp();
    }

    private void Update()
    {
        UpdateXpSlider();
    }

    private void UpdateXpSlider()
    {
        xpSlider.value = (xp / requiredXP) * 100;
    }

    private void CalculateRequiredXp()
    {
        requiredXP = (float)Math.Pow((level / 0.3f),2);
    }

    private void CheckLevelUp()
    {
        if (xp >= requiredXP)
        {
            level++;
            xp = 0;
            CalculateRequiredXp();
            levelUpPanel.SetActive(true);
            Time.timeScale = 0;
        }

        levelText.text = level.ToString();
    }
    private void Start()
    {
        CalculateRequiredXp();
    }
}
