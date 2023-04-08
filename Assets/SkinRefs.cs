using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRefs : MonoBehaviour
{
    public static SkinRefs instance;

    public Skin Bill;
    public Skin superBill;

    private bool loaded;

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

    public void LoadSkins()
    {
        if (PlayerData.instance.persistentData.superBill)
        {
            superBill.unlocked = true;
        }

        if (!loaded)
        {
            SkinSelector.instance.skins.Add(Bill);
            SkinSelector.instance.skins.Add(superBill);
        }
    }
}
