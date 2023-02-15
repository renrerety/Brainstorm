using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public static PlayerWeapons Instance;
    
    [SerializeField] private WeaponMaster starterWeapon;
    [SerializeField] private WeaponMaster starterWeapon2;

    [SerializeField] public List<ScriptableObject> weapons = new List<ScriptableObject>();
    
    [Header("Fireball Refs")]
    [SerializeField] GameObject  fireballWeaponObj;
    [SerializeField] float fireballCooldown;
    [SerializeField] public int fireballDamage;
    public int fireballMaxHit;
    [SerializeField] string fireballName;
    [SerializeField] string fireballDesc;
    [SerializeField] [TextArea] string fireballLevelUpDesc;
    [SerializeField] Sprite fireballImage;
    
    
    [Header("Throwing Knife Refs")]
    [SerializeField] GameObject  throwingKnifeWeaponObj;
    [SerializeField] float throwingKnifeCooldown;
    [SerializeField] public int throwingKnifeDamage;
    public int throwingKnifeMaxHit;
    [SerializeField] string throwingKnifeName;
    [SerializeField] string throwingKnifeDesc;
    [SerializeField] [TextArea] string throwingKnifeLevelUpDesc;
    [SerializeField] Sprite throwingKnifeImage;
    
    

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
        AddWeaponToList(starterWeapon);
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
        WeaponMaster weaponInst;
        switch (weapon.name)
        {
            case "Fireball":
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                weaponInst.Init(fireballWeaponObj,fireballCooldown,fireballDamage,fireballMaxHit,fireballName,fireballDesc,fireballLevelUpDesc,fireballImage);
                break;
            case "Throwing Knife":
                weaponInst = ScriptableObject.CreateInstance<ThrowingKnifeWeapon>();
                weaponInst.Init(throwingKnifeWeaponObj,throwingKnifeCooldown,throwingKnifeDamage,throwingKnifeMaxHit,throwingKnifeName,throwingKnifeDesc,throwingKnifeLevelUpDesc,throwingKnifeImage);
                break;
            default:
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                weaponInst.Init(fireballWeaponObj,fireballCooldown,fireballDamage,fireballMaxHit,fireballName,fireballDesc,fireballLevelUpDesc,fireballImage);
                break;
        }
        
        
        weaponInst.timer = weaponInst.cooldown;
        weaponInst.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(weaponInst);
    }
}
