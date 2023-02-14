using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private WeaponMaster starterWeapon;
    [SerializeField] private WeaponMaster starterWeapon2;

    [SerializeField] private List<ScriptableObject> weapons = new List<ScriptableObject>();
    // Start is called before the first frame update
    void Start()
    {
        AddWeaponToList(starterWeapon);
        AddWeaponToList(starterWeapon2);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WeaponMaster weapon in weapons)
        {
            weapon.timer -= Time.deltaTime;
            if (weapon.timer <= 0)
            {
                weapon.Attack();
            }
        }
    }

    public void AddWeaponToList(WeaponMaster weapon)
    {
        weapon.timer = weapon.cooldown;
        weapon.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(weapon);
    }
}
