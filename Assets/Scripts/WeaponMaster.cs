using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponMaster : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject weaponObj;
    public float cooldown;

    protected float timer;
    public abstract void Attack();

    public virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Attack();
        }
    }
    public virtual void Start()
    {
        Debug.Log("start");
        timer = cooldown;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
