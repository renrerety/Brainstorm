using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyFactory : AbstractFactory
{
    public static MediumEnemyFactory instance;
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
