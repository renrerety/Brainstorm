using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponMaster
{
    public override void Attack()
    {
        GameObject knife = TakeWeaponFromPool();

        //knife.transform.rotation = new Quaternion(PlayerMovement.lastDirection.y * 180,PlayerMovement.lastDirection.z * 180 , PlayerMovement.lastDirection.x * 180, 0);
        knife.transform.Rotate(new Vector3(0, PlayerMovement.lastDirection.y * 180, PlayerMovement.lastDirection.z*90));
        knife.transform.position = playerTransform.position;

        timer = cooldown;
    }
}
