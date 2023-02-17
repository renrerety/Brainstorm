using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Fireball")]
public class FireballWeapon : WeaponMaster
{
    [SerializeField] AudioClip fireballClip;

    Transform nearestEnemy;
    float temp = 100f;

    public override void Attack()
    {
        FindNearestEnemy();

        GameObject fireball = FireballPool.Instance.TakeFireballFromPool();
        fireball.transform.position = playerTransform.position;
        fireball.transform.right = nearestEnemy.position - playerTransform.position;
        
        timer = cooldown;
    }

    public override void LevelUp()
    {
        WeaponMaster weapon = PlayerWeapons.Instance.FindWeapon("Fireball");
        weapon.maxHit += 2;
        weapon.cooldown -= 0.2f;
        if (weapon.cooldown < 1)
        {
            weapon.cooldown = 1;
        }
        weapon.damage += 2;
    }
    
    public override void ReturnWeaponToPool(GameObject weapon)
    {
        FireballPool.Instance.ReturnFireballToPool(weapon);
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
