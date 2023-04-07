using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class PlayerSkins
{
    public List<Skin> skinList;

    public PlayerSkins(List<Skin> skins)
    {
        this.skinList = skins;
    }
}
