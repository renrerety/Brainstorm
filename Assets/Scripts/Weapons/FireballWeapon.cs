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

    private float spread = 1f;
    public override void Attack()
    {
        for (int i = 0; i < attackAmount; i++)
        {
            Transform nearestEnemy = EnemySpawner.Instance.FindNearestEnemy(200);

            GameObject fireball = _fireballPool.TakeFireballFromPool();
            fireball.transform.position = playerTransform.position;
            fireball.transform.right = ((nearestEnemy.position - playerTransform.position) / 2 );
            fireball.transform.Rotate(new Vector3(0,0,i*spread /2 *attackAmount));
        }

        timer = cooldown;
    }

    public override void LevelUp()
    {
        cooldown -= 0.2f;
        attackAmount += 1;
        if (cooldown < 2f)
        {
            cooldown = 2f;
        }
        damage += 1;
    }
    
    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _fireballPool.ReturnFireballToPool(weapon);
    }
}
