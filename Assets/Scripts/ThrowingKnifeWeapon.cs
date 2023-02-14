using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponMaster
{
    public override void Attack()
    {
        GameObject knife = TakeWeaponFromPool();

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
}
