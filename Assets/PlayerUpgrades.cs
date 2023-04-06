using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public sealed class PlayerUpgrades
{

    public int hpUp;
    public int damageUp;
    public int speedUp;
    public int xpUp;

    public PlayerUpgrades(int hpUp, int damageUp, int speedUp, int xpUp)
    {
        this.hpUp = hpUp;
        this.damageUp = damageUp;
        this.speedUp = speedUp;
        this.xpUp = xpUp;
    }
}
