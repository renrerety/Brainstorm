using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Throwing Knife")]
public class ThrowingKnifeWeapon : WeaponMaster
{
    public override void Attack()
    {
        
        Debug.Log("Attack knife");
        GameObject knife = ThrowingKnifePool.Instance.TakeThrowingKnifeFromPool();

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

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        ThrowingKnifePool.Instance.ReturnThrowingKnifeToPool(weapon);
    }
}
