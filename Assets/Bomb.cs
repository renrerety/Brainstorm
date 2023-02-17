using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnEnable()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        float delay = 1.5f;

        WeaponMaster weapon = PlayerWeapons.Instance.FindWeapon("Bomb");
        delay = (weapon as BombWeapon).delay;
        yield return new WaitForSeconds(delay);
        
        GetComponent<AudioSource>().Play();
        explosion.SetActive(true);
    }
}
