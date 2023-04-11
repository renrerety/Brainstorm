using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Bomb : MonoBehaviour
{
    [HideInInspector] public BombPool _bombPool;
    
    public GameObject explosionObj;
    private void Start()
    {
        Explosion explosion = explosionObj.GetComponent<Explosion>();
        explosion._bombPool = _bombPool;
    }

    private void OnEnable()
    {
        WeaponMaster weapon = PlayerWeapons.Instance.FindWeapon("Bomb");
        if (weapon.name == "Bomb")
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        float delay = 1.5f;

        WeaponMaster weapon = PlayerWeapons.Instance.FindWeapon("Bomb");
        delay = (weapon as BombWeapon).delay;
        yield return new WaitForSeconds(delay);
        
        GetComponent<AudioSource>().Play();
        explosionObj.SetActive(true);
    }
}
