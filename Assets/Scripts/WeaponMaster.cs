using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponMaster : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject weaponObj;
    public float cooldown;
    public int damage;

    public float timer;

    int index;
    List<GameObject> weaponPoolList = new List<GameObject>();
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
        CreatePool();
        timer = cooldown;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void CreatePool()
    {
        GameObject weaponPool = new GameObject(weaponObj.ToString());
        for (int i = 0; i < 25; i++)
        {

            GameObject weapon = Instantiate(weaponObj, weaponPool.transform);
            weapon.SetActive(false);
            weaponPoolList.Add(weapon);
        }
    }
    public GameObject TakeWeaponFromPool()
    {
        if (index >= weaponPoolList.Count)
        {
            index = 0;
        }
        GameObject fireball = weaponPoolList[index++];
        fireball.SetActive(true);
        fireball.GetComponent<Projectile>().damage = damage;
        return fireball;
    }
    public void ReturnWeaponToPool(GameObject weapon)
    {
        weapon.SetActive(false);
        weapon.transform.position = Vector3.zero;
        weapon.transform.rotation = Quaternion.identity;
    }
}
