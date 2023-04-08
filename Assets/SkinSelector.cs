using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public static SkinSelector instance;

    public Sprite lockedSkin;
    
    public List<Skin> skins;
    public int index;

    public void ResetIndex()
    {
        index = 0;
    }
    private void Awake()
    {
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
    public void UpdateCurrentSkin()
    {
        PlayerData.instance.currentSkin = skins[index].type;
    }

    private void UpdateDisplay()
    {
        if (skins[index].unlocked)
        {
            GameObject.Find("SkinImage").GetComponent<Image>().sprite = skins[index].sprite;
        }
        else
        {
            GameObject.Find("SkinImage").GetComponent<Image>().sprite = skins[index].lockedSprite;
        }
        UpdateCurrentSkin();
    }

    private IEnumerator LoadSkins()
    {
        yield return new WaitForEndOfFrame();
        SkinRefs.instance.LoadSkins();

        if (PlayerData.instance.persistentData.superBill)
        {
            Debug.Log("SUper bill");
            skins[1].unlocked = true;
        }
    }

    public void StartLoadSkins()
    {
        StartCoroutine(LoadSkins());
    }

    private void Start()
    {
        ResetIndex();
        StartLoadSkins();
    }
}
[Serializable]
public class Skin
{
    public Sprite sprite;
    public Sprite lockedSprite;
    public bool unlocked;
    public SkinList type;
}
