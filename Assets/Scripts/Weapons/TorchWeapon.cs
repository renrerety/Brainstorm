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
        circle.transform.localScale = new Vector3(
                    circle.transform.localScale.x + 0.05f,
                    circle.transform.localScale.y + 0.05f,
                    circle.transform.localScale.z + 0.05f);

        cooldown -= 0.15f;
        if (cooldown <= 2)
        {
            cooldown = 2;
        }
       duration += 0.5f;
       damage += 1;
        if (duration > 2.5f)
        {
            duration = 2.5f;
        }
    }

    public override void ReturnWeaponToPool(GameObject weapon)
    {
        //No need to implement pooling
    }
}
