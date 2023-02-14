using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpGem : MonoBehaviour
{
    [SerializeField] private float xpAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerLevel>().AddXp(xpAmount);
            XpPool.Instance.ReturnXpToPool(gameObject);
        }
    }
}
