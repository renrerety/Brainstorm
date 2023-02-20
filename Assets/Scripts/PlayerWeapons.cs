using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

public class PlayerWeapons : MonoBehaviour
{
    //public static PlayerWeapons Instance;

    [SerializeField] public List<ScriptableObject> weapons = new List<ScriptableObject>();

    [Inject] private FireballPool _fireballPool;
    [Inject] private ThrowingKnifePool _throwingKnifePool;
    [Inject] private BombPool _bombPool;

// Start is called before the first frame update
    void Start()
    {
        AddWeaponToList("Shield");
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

    public WeaponMaster FindWeapon(string weaponName)
    {
        foreach (WeaponMaster weapon in weapons)
        {
            if (weapon.name == weaponName)
            {
                return weapon;
            }
        }
        return null;
    }

    public void AddWeaponToList(string name)
    {
        WeaponMaster weaponInst;
        WeaponMaster weaponRef = WeaponRefs.Instance.FindWeaponRef(name);
        switch (name)
        {
            case "Fireball":
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                (weaponInst as FireballWeapon)._fireballPool = _fireballPool;
                break;
            case "Throwing Knife":
                weaponInst = ScriptableObject.CreateInstance<ThrowingKnifeWeapon>();
                (weaponInst as ThrowingKnifeWeapon)._throwingKnifePool = _throwingKnifePool;
                break;
            case "Torch":
                weaponInst = ScriptableObject.CreateInstance<TorchWeapon>();
                break;
            case "Bomb":
                weaponInst = ScriptableObject.CreateInstance<BombWeapon>();
                (weaponInst as BombWeapon)._bombPool = _bombPool;
                break;
            case "Shield":
                weaponInst = ScriptableObject.CreateInstance<ShieldWeapon>();
                break;
            default:
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                break;
        }
        weaponInst._playerWeapons = this;
        weaponInst.Init(weaponRef.weaponObj,weaponRef.delay,weaponRef.duration,weaponRef.cooldown,weaponRef.damage,weaponRef.maxHit,weaponRef.name,weaponRef.desc,weaponRef.levelUpDesc,weaponRef.image);
        
        
        weaponInst.timer = weaponInst.cooldown;
        weaponInst.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(weaponInst);
    }
}
