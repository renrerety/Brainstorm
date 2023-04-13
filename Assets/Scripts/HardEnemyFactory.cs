using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemyFactory : AbstractFactory
{
    public static HardEnemyFactory instance;
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
