using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FireCircleAOE : MonoBehaviour
{
    public PlayerWeapons _playerWeapons;
    private bool enemyInZone;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            StartCoroutine(DamageOverTime(col.GetComponent<AIMaster>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(DamageOverTime(other.GetComponent<AIMaster>()));
    }

    IEnumerator DamageOverTime(AIMaster enemy)
    {
        enemy.TakeDamage(_playerWeapons.FindWeapon("Torch").damage);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DamageOverTime(enemy));
    }
}
