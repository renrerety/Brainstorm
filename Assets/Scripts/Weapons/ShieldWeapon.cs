using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Shield")]
public class ShieldWeapon : WeaponMaster
{
    private GameObject shield;
    public bool isActive;
    public override void Attack()
    {
        if(shield == null)
        {
            shield = Instantiate(weaponObj);
            shield.transform.SetParent(playerTransform);
            shield.transform.localPosition = Vector3.zero;
            MonoBehaviourRef.Instance.StartCoroutine(ToggleShield());
        }
    }

    IEnumerator ToggleShield()
    {
        isActive = true;
        shield.SetActive(true);
        
        yield return new WaitForSeconds(duration);

        isActive = false;
        shield.SetActive(false);
        timer = cooldown;

        yield return new WaitForSeconds(cooldown);
        MonoBehaviourRef.Instance.StartCoroutine(ToggleShield());
    }

    public override void LevelUp()
    {
        cooldown -= 0.2f;
        duration += 0.2f;

        if (cooldown == 1)
        {
            cooldown = 1;
        }
        if (duration == 5)
        {
            duration = 5;
        }
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        //No need for pooling
    }
}
