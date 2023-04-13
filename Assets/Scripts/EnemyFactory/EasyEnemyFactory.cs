using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyFactory : AbstractFactory
{
    public static EasyEnemyFactory instance;
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
