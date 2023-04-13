using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthProxy : MonoBehaviour, IPlayerHealth
{
    public static PlayerHealthProxy instance;

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

    public void TakeDamage(float damage)
    {
        ShieldWeapon shield = (PlayerWeapons.Instance.FindWeapon("Shield") as ShieldWeapon);
        if (shield && shield.isActive)
        {
            return;
        }
        else
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }
}
