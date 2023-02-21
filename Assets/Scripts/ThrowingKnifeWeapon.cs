using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Throwing Knife")]
public class ThrowingKnifeWeapon : WeaponMaster
{
    [HideInInspector] public ThrowingKnifePool _throwingKnifePool;
    public override void Attack()
    {
        
        Debug.Log("Attack knife");
        GameObject knife = _throwingKnifePool.TakeThrowingKnifeFromPool();

        int yRot;
        int zRot;
  
        if (PlayerMovement.lastDirection.y == -1)
        {
            yRot = 180;
        }
        else
        {
            yRot = 0;
        }
        zRot = (int)(PlayerMovement.lastDirection.z * 90);

        knife.transform.Rotate(new Vector3(0, yRot, zRot));
        knife.transform.position = playerTransform.position;

        timer = cooldown;
    }

    public override void LevelUp()
    {
        foreach (WeaponMaster weapon in _playerWeapons.weapons)
                {
                    if (weapon.name == "Throwing Knife")
                    {
                        weapon.maxHit += 1;
                        weapon.cooldown -= 0.2f;
                        if (weapon.cooldown < 1)
                        {
                            weapon.cooldown = 1;
                        }
                        weapon.damage += 2;
                    }
                }
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _throwingKnifePool.ReturnThrowingKnifeToPool(weapon);
    }
}
