using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpGem : MonoBehaviour
{
    [HideInInspector] public XpPool _xpPool;
    
    [SerializeField] private float xpAmount;
    [SerializeField] private AudioClip expClip;

    [HideInInspector] public bool moveToward;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PickupZone"))
        {
            moveToward = true;
        }
    }

    private void Update()
    {
        if (moveToward)
        {
            transform.position  = Vector3.MoveTowards(transform.position, player.position,Time.deltaTime * 5);
            if (Vector3.Distance(transform.position,player.position)<0.5f)
            {
                player.GetComponent<PlayerLevel>().AddXp(xpAmount);
                _xpPool.ReturnXpToPool(gameObject);
            }
        }
    }
}
