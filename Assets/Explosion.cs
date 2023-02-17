using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
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
        BombPool.Instance.ReturnBombToPool(transform.parent.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
