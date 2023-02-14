using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponMaster : ScriptableObject
{
    public Transform playerTransform;
    public GameObject weaponObj;
    public float cooldown;
    public int damage;
    
    [HideInInspector] public float timer;

    int index;
    List<GameObject> weaponPoolList = new List<GameObject>();

    public abstract void Attack();

    public abstract void ReturnWeaponToPool(GameObject weapon);
}
