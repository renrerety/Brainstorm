using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LaserRay : MonoBehaviour
{
    [HideInInspector] public PlayerWeapons _playerWeapons;
    [HideInInspector] public LaserPool _laserPool;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AIMaster>().TakeDamage(_playerWeapons.FindWeapon("Laser Gun").damage);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyLaser());
    }

    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(0.2f);
        _laserPool.ReturnObjectToPool(gameObject);
    }

    private void OnDestroy()
    {
        
    }

    private void Start()
    {
    }
}
