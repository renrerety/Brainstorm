using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthProxy : MonoBehaviour, IPlayerHealth
{
    [Inject] private PlayerWeapons _playerWeapons;
    [Inject] private PlayerHealth _playerHealth;

    public void TakeDamage(float damage)
    {
        ShieldWeapon shield = (_playerWeapons.FindWeapon("Shield") as ShieldWeapon);
        if (shield && shield.isActive)
        {
            return;
        }
        else
        {
            _playerHealth.TakeDamage(damage);
        }
    }
}
