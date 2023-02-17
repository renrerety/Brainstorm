using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircleAOE : MonoBehaviour
{
    
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
        enemy.TakeDamage(PlayerWeapons.Instance.FindWeapon("Torch").damage);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DamageOverTime(enemy));
    }
}
