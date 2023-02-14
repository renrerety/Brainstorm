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

    public void AddXp(float xpToAdd)
    {
        xp += xpToAdd;
        
        CheckLevelUp();
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
        }

        levelText.text = level.ToString();
    }
    private void Start()
    {
        CalculateRequiredXp();
    }
}