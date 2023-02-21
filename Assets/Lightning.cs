using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public PlayerWeapons _playerWeapons;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            int damage = _playerWeapons.FindWeapon("Lightning Rod").damage;
            col.GetComponent<AIMaster>().TakeDamage(damage);
        }
    }
}
