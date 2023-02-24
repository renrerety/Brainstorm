using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Lightning Rod")]
public class LightningRodWeapon : WeaponMaster
{
    public LightningPool _lightningPool;
    public override void Attack()
    {
        for (int i = 0; i < attackAmount; i++)
        {
            Vector3 randomPos = _enemySpawner.FindRandomEnemy().position;
            GameObject obj = _lightningPool.GetPooledObject(randomPos);

            MonoBehaviourRef.Instance.StartCoroutine(DisableLightning(obj));
        }
        
        timer = cooldown;
    }

    private IEnumerator DisableLightning(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        _lightningPool.ReturnObjectToPool(obj);
    }

    public override void LevelUp()
    {
        foreach (GameObject lightning in _lightningPool.lightningPoolList)
        {
            BoxCollider2D col = lightning.GetComponent<BoxCollider2D>();
            col.size = new Vector2(col.size.x + 0.1f, col.size.y + 0.1f);
        }

        attackAmount += 1;
        damage += 2;
        cooldown -= 0.5f;
        if (cooldown < 1)
        {
            cooldown = 1;
        }
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        _lightningPool.ReturnObjectToPool(weapon);
    }
}
