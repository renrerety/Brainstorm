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

        fireball.GetComponent<AudioSource>().PlayOneShot(fireballClip);

        timer = cooldown;
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
