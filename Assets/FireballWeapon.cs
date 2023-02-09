using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FireballWeapon : WeaponMaster
{
    [SerializeField] AudioClip fireballClip;

    Transform nearestEnemy;
    float temp = 100f;
    int index;
    List<GameObject> fireballPoolList = new List<GameObject>();

    public override void Start()
    {
        base.Start();
        CreatePool();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        FindNearestEnemy();

        GameObject fireball = TakeFireballFromPool();
        fireball.transform.position = playerTransform.position;
        fireball.transform.right = nearestEnemy.position - transform.position;

        fireball.GetComponent<AudioSource>().PlayOneShot(fireballClip);

        timer = cooldown;
    }
    public void CreatePool()
    {
        GameObject fireballPool = new GameObject("FireballPool");
        for (int i = 0; i < 25; i++)
        {

            GameObject fireball = Instantiate(weaponObj, fireballPool.transform);
            fireball.SetActive(false);
            fireballPoolList.Add(fireball);
        }
    }
    public GameObject TakeFireballFromPool()
    {
        if(index >= fireballPoolList.Count)
        {
            index = 0;
        }
        GameObject fireball = fireballPoolList[index++];
        fireball.SetActive(true);
        fireball.GetComponent<Fireball>().damage = damage;
        return fireball;
    }
    public void ReturnFireballToPool(GameObject fireball)
    {
        fireball.SetActive(false);
        fireball.transform.position = Vector3.zero;
        fireball.transform.rotation = Quaternion.identity;
    }
    void FindNearestEnemy()
    {
        foreach (GameObject enemy in EnemySpawner.Instance.activeEnemyList)
        {
            float distance = Vector2.Distance(playerTransform.position, enemy.transform.position);
            if (distance < temp)
            {
                nearestEnemy = enemy.transform;
                temp = distance;
            }
        }
        temp = 100;
    }
}
