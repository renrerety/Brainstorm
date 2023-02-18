using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyFactory : AbstractFactory
{
    public static EasyEnemyFactory Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }
}
