using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRefs : MonoBehaviour
{
    public static SkinRefs instance;

    public Skin superBill;

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
}
