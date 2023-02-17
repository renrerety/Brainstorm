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
    
    [Header("Torch Refs")]
    [SerializeField] GameObject  torchWeaponObj;
    [SerializeField] float torchCooldown;
    [SerializeField] public int torchDamage;
    [SerializeField] private int torchDuration;
    public int torchMaxHit;
    [SerializeField] string torchName;
    [SerializeField] string torchDesc;
    [SerializeField] [TextArea] string torchLevelUpDesc;
    [SerializeField] Sprite torchImage;
    
    [Header("Bomb Refs")]
    [SerializeField] GameObject  bombWeaponObj;
    [SerializeField] float bombCooldown;
    [SerializeField] public int bombDamage;
    [SerializeField] private float bombDelay;
    public int bombMaxHit;
    [SerializeField] string bombName;
    [SerializeField] string bombDesc;
    [SerializeField] [TextArea] string bombLevelUpDesc;
    [SerializeField] Sprite bombImage;
    
    

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
        AddWeaponToList("Bomb");
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
        switch (name)
        {
            case "Fireball":
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                weaponInst.Init(fireballWeaponObj,fireballCooldown,fireballDamage,fireballMaxHit,fireballName,fireballDesc,fireballLevelUpDesc,fireballImage);
                break;
            case "Throwing Knife":
                weaponInst = ScriptableObject.CreateInstance<ThrowingKnifeWeapon>();
                weaponInst.Init(throwingKnifeWeaponObj,throwingKnifeCooldown,throwingKnifeDamage,throwingKnifeMaxHit,throwingKnifeName,throwingKnifeDesc,throwingKnifeLevelUpDesc,throwingKnifeImage);
                break;
            case "Torch":
                weaponInst = ScriptableObject.CreateInstance<TorchWeapon>();
                weaponInst.Init(torchWeaponObj,torchCooldown,torchDamage,torchMaxHit,torchName,torchDesc,torchLevelUpDesc,torchImage);
                (weaponInst as TorchWeapon).duration = torchDuration;
                break;
            case "Bomb":
                weaponInst = ScriptableObject.CreateInstance<BombWeapon>();
                weaponInst.Init(bombWeaponObj,bombCooldown,bombDamage,bombMaxHit,bombName,bombDesc,bombLevelUpDesc,bombImage);
                (weaponInst as BombWeapon).delay = bombDelay;
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
