using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Bomb")]
public class BombWeapon : WeaponMaster
{
    public float delay;
    public override void Attack()
    {
        GameObject bomb =  BombPool.Instance.TakeBombFromPool();
        bomb.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        timer = cooldown;
    }

    public override void LevelUp()
    {
        BombWeapon bomb = (PlayerWeapons.Instance.FindWeapon("Bomb") as BombWeapon);
        bomb.delay -= 0.2f;
        bomb.cooldown -= 0.5f;
        bomb.damage += 2;

        foreach (GameObject bombObj in BombPool.Instance.bombPoolList)
        {
            GameObject explosion = bombObj.GetComponent<Bomb>().explosion;
            explosion.transform.localScale = new Vector3(
                explosion.transform.localScale.x + 0.2f,
                explosion.transform.localScale.y + 0.2f, 
                explosion.transform.localScale.z + 0.2f);
        }
        
        if (delay < 2f)
        {
            delay = 2f;
        }

        if (cooldown < 1)
        {
            cooldown = 1;
        }
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        throw new System.NotImplementedException();
    }
}
