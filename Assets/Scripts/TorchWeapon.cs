using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Weapons/Torch")]
public class TorchWeapon : WeaponMaster
{
    public GameObject circle;

    private bool isActivated = false;
    public override void Attack()
    {
        if (!isActivated)
        {
            if (circle == null)
            {
                circle = Instantiate(weaponObj, GameObject.FindGameObjectWithTag("Player").transform);
                circle.GetComponent<FireCircleAOE>()._playerWeapons = _playerWeapons;
                circle.transform.localPosition = Vector3.zero;
                circle.transform.rotation = quaternion.identity;
                MonoBehaviourRef.Instance.StartCoroutine(ActivateCircle());
                isActivated = true;
            }
        }
    }

    IEnumerator ActivateCircle()
    {
        circle.SetActive(true);
        yield return new WaitForSeconds(duration);
        circle.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        MonoBehaviourRef.Instance.StartCoroutine(ActivateCircle());
    }

    public override void LevelUp()
    {

        WeaponMaster weapon = _playerWeapons.FindWeapon("Torch");
        
        (weapon as TorchWeapon).circle.transform.localScale = new Vector3(
                    (weapon as TorchWeapon).circle.transform.localScale.x + 0.5f,
                    (weapon as TorchWeapon).circle.transform.localScale.y + 0.5f,
                    (weapon as TorchWeapon).circle.transform.localScale.z + 0.5f);

        (weapon as TorchWeapon).duration += 0.5f;
        if ((weapon as TorchWeapon).duration > 2.5f)
        {
            (weapon as TorchWeapon).duration = 2.5f;
        }
                
        weapon.damage += 1;
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        //No need to implement pooling
    }
}
