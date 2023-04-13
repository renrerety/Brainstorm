using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public BombPool _bombPool;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("Explosion");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<AIMaster>().TakeDamage(PlayerWeapons.Instance.FindWeapon("Bomb").damage);
        }
    }

    public void ReturnToPool()
    {
        _bombPool.ReturnBombToPool(transform.parent.gameObject);
    }
}
