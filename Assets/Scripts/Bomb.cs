using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Bomb : MonoBehaviour
{
    [HideInInspector] public PlayerWeapons _playerWeapons;
    [HideInInspector] public BombPool _bombPool;
    
    public GameObject explosionObj;
    private void Start()
    {
        Explosion explosion = explosionObj.GetComponent<Explosion>();
        explosion._bombPool = _bombPool;
        explosion._playerWeapons = _playerWeapons;
    }

    private void OnEnable()
    {
        if (_playerWeapons.FindWeapon("Bomb"))
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        float delay = 1.5f;

        WeaponMaster weapon = _playerWeapons.FindWeapon("Bomb");
        delay = (weapon as BombWeapon).delay;
        yield return new WaitForSeconds(delay);
        
        GetComponent<AudioSource>().Play();
        explosionObj.SetActive(true);
    }
}
