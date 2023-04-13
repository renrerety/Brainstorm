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

    private FireballPool _fireballPool;
    private ThrowingKnifePool _throwingKnifePool;
    private BombPool _bombPool;
    private LightningPool _lightningPool;
    private LaserPool _laserPool;

    public int criticalChance;

    private bool initialized;

    private void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Init(FireballPool fireballPool, ThrowingKnifePool throwingKnifePool,BombPool bombPool,LightningPool lightningPool,LaserPool laserPool)
    {
        initialized = true;
        this._fireballPool = fireballPool;
        this._throwingKnifePool = throwingKnifePool;
        this._bombPool = bombPool;
        this._lightningPool = lightningPool;
        this._laserPool = laserPool;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (initialized)
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
            case "Lightning Wand":
                weaponInst = ScriptableObject.CreateInstance<LightningRodWeapon>();
                (weaponInst as LightningRodWeapon)._lightningPool = _lightningPool;
                break;
            case "Laser Gun":
                weaponInst = ScriptableObject.CreateInstance<LaserGunWeapon>();
                (weaponInst as LaserGunWeapon)._laserPool = _laserPool;
                break;
            default:
                weaponInst = ScriptableObject.CreateInstance<FireballWeapon>();
                break;
        }
        weaponInst._playerWeapons = this;
        weaponInst.Init(weaponRef.translateKey,weaponRef.weaponObj,weaponRef.delay,weaponRef.duration,weaponRef.cooldown,weaponRef.damage,weaponRef.attackAmount,weaponRef.maxHit,weaponRef.name,weaponRef.desc,weaponRef.levelUpDesc,weaponRef.image);
        
        
        weaponInst.timer = weaponInst.cooldown;
        weaponInst.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weapons.Add(weaponInst);
    }
}
