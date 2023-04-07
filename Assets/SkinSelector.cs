using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public static SkinSelector instance;

    public List<Skin> skins;
    [SerializeField] private Image img;
    public int index;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    public void Increment()
    {
        if (index < skins.Count-1)
        {
            index++;
            UpdateDisplay();
        }
    }

    public void Decrement()
    {
        if (index > 0)
        {
            index--;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        img.sprite = skins[index].sprite;
    }

    public void LoadSkins()
    {
        foreach (Skin skin in PlayerData.instance.persistentData.skins.skinList)
        {
            if (skin.unlocked)
            {
                skins.Add(skin);
            }
        }
    }

    private void Start()
    {
        LoadSkins();
        UpdateDisplay();
    }
}
[Serializable]
public class Skin
{
    public Sprite sprite;
    public bool unlocked;
}
