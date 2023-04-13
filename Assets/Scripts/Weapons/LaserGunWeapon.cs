using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Weapons/Laser Gun")]
public class LaserGunWeapon : WeaponMaster
{
    [HideInInspector] public LaserPool _laserPool;
    public override void Attack()
    {
        for (int i = 0; i < attackAmount; i++)
        {
            Transform nearestEnemy;
            if (EnemySpawner.Instance.activeEnemyList.Count > 0)
            {
                try
                {
                    nearestEnemy = EnemySpawner.Instance.activeEnemyList[i].transform;
                    if (Vector2.Distance(nearestEnemy.position, playerTransform.position) >= 5)
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    return;
                }
            
                if (nearestEnemy != null)
                {
                    GameObject laser = _laserPool.GetPooledObject();
                    laser.transform.position = playerTransform.position;
        
                    SpriteRenderer _spriteRenderer = laser.GetComponent<SpriteRenderer>();
        
                    laser.transform.right = ((nearestEnemy.position - playerTransform.position) / 2 );

                    float sizeX = ((Vector2.Distance(playerTransform.position, nearestEnemy.position)) / 3);
                    float sizeY = _spriteRenderer.size.y;
                    _spriteRenderer.size = new Vector2(sizeX, sizeY);
                    nearestEnemy.GetComponent<AIMaster>().TakeDamage(damage);

                    timer = cooldown;
                }
            }
        }
        
    }

    public override void LevelUp()
    {
        cooldown -= 0.15f;
        if (cooldown < 1)
        {
            cooldown = 1;
        }
        attackAmount += 1;
        damage += 1;
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _laserPool.ReturnObjectToPool(weapon);
    }
}
