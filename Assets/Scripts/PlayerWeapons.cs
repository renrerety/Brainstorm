using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public static PlayerWeapons Instance;

    [SerializeField] public List<ScriptableObject> weapons = new List<ScriptableObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AddWeaponToList("Fireball");
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
                break;
            case "Throwing Knife":
                weaponInst = ScriptableObject.CreateInstance<ThrowingKnifeWeapon>();
                break;
            case "Torch":
                weaponInst = ScriptableObject.CreateInstance<TorchWeapon>();
                break;
            case "Bomb":
                weaponInst = ScriptableObject.CreateInstance<BombWeapon>();
                break;
            default:
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                break;
        }
        
        weaponInst.Init(weaponRef.weaponObj,weaponRef.delay,weaponRef.duration,weaponRef.cooldown,weaponRef.damage,weaponRef.maxHit,weaponRef.name,weaponRef.desc,weaponRef.levelUpDesc,weaponRef.image);
        
        
        weaponInst.timer = weaponInst.cooldown;
        weaponInst.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(weaponInst);
    }
}
