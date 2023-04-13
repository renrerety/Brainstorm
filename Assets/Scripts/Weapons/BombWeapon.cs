using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Bomb")]
public class BombWeapon : WeaponMaster
{
    [HideInInspector] public BombPool _bombPool;
    public override void Attack()
    {
        GameObject bomb =  _bombPool.TakeBombFromPool();
        bomb.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        timer = cooldown;
    }

    public override void LevelUp()
    {
        delay -= 0.2f;
        cooldown -= 0.5f;
        damage += 3;

        foreach (GameObject bombObj in _bombPool.bombPoolList)
        {
            GameObject explosion = bombObj.GetComponent<Bomb>().explosionObj;
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
