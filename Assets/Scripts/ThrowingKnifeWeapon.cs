using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Throwing Knife")]
public class ThrowingKnifeWeapon : WeaponMaster
{
    [HideInInspector] public ThrowingKnifePool _throwingKnifePool;

    private float spread = 0.25f;
    public override void Attack()
    {

        for (int i = 0; i < attackAmount; i++)
        {
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
            if (PlayerMovement.lastDirection.z == 0)
            {
                float posX = playerTransform.position.x + Random.Range(0f, 1f);
                float posY = (playerTransform.position.y) + (i * spread);
                posY -= (spread * attackAmount) / 2;
                knife.transform.position = new Vector3(posX,posY);
            }
            else
            {
                float posX = (playerTransform.position.x) + (i * spread);
                posX -= (spread * attackAmount) / 2;
                float posY = playerTransform.position.y + Random.Range(0f, 1f);
                knife.transform.position = new Vector3(posX + 0.1f,posY);
            }
        }
        timer = cooldown;
    }

    public override void LevelUp()
    {
        WeaponMaster knife = _playerWeapons.FindWeapon("Throwing Knife");
        
        knife.maxHit += 1;
        knife.cooldown -= 0.1f;
        knife.attackAmount += 1;
        
            if (knife.cooldown < 0.75f)
            {
                knife.cooldown = 0.75f;
            }
        knife.damage += 1;
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _throwingKnifePool.ReturnThrowingKnifeToPool(weapon);
    }
}
