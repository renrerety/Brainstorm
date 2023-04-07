using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public static SkinSelector instance;

    public Sprite lockedSkin;
    
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

    public IEnumerator LoadSkins()
    {
        yield return new WaitForEndOfFrame();
        SkinRefs.instance.LoadSkins();

        if (PlayerData.instance.persistentData.superBill)
        {
            Debug.Log("SUper bill");
            skins[1].unlocked = true;
        }
        foreach (var skin in skins)
        {
            if (!skin.unlocked)
            {
                skin.sprite = lockedSkin;
            }
        }
    }

    private void Start()
    {
        StartCoroutine(LoadSkins());
    }
}
[Serializable]
public class Skin
{
    public Sprite sprite;
    public bool unlocked;
}
