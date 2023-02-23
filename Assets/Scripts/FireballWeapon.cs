using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName ="Custom/Weapons/Fireball")]
public class FireballWeapon : WeaponMaster
{
    public FireballPool _fireballPool;
    [SerializeField] AudioClip fireballClip;
    Transform nearestEnemy;
    float temp = 100f;

    private int spread = 3;
    public override void Attack()
    {
        for (int i = 0; i < attackAmount; i++)
        {
            Transform nearestEnemy = _enemySpawner.FindNearestEnemy();

            GameObject fireball = _fireballPool.TakeFireballFromPool();
            fireball.transform.position = playerTransform.position;
            fireball.transform.right = ((nearestEnemy.position - playerTransform.position) / 2 );
            fireball.transform.Rotate(new Vector3(0,0,i*spread /2 *attackAmount));
        }

        timer = cooldown;
    }

    public override void LevelUp()
    {
        WeaponMaster weapon =_playerWeapons.FindWeapon("Fireball");
        weapon.maxHit += 1;
        weapon.cooldown -= 0.2f;
        weapon.attackAmount += 1;
        if (weapon.cooldown < 0.5f)
        {
            weapon.cooldown = 0.5f;
        }
        weapon.damage += 3;
    }
    
    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _fireballPool.ReturnFireballToPool(weapon);
    }
}
