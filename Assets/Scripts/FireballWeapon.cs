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

        GameObject fireball = TakeWeaponFromPool();
        fireball.transform.position = playerTransform.position;
        fireball.transform.right = nearestEnemy.position - transform.position;

        fireball.GetComponent<AudioSource>().PlayOneShot(fireballClip);

        timer = cooldown;
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
