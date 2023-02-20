using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelOption : MonoBehaviour
{
    private WeaponMaster weapon;

    [SerializeField] private Image image;
    [SerializeField] private Text name;
    [SerializeField] private Text desc;

    [Inject] private PlayerWeapons _playerWeapons;

    private void OnEnable()
    {
        int rng = UnityEngine.Random.Range(0, WeaponRefs.Instance.weaponRefs.Count);
        weapon = WeaponRefs.Instance.weaponRefs[rng];
        
        image.sprite = weapon.image;
        name.text = weapon.name;
        desc.text = weapon.desc;
        
        
        foreach (WeaponMaster playerWeapon in _playerWeapons.weapons)
        {
            if (weapon.name == playerWeapon.name)
            {
                desc.text = playerWeapon.levelUpDesc;
            }
        }
    }

    public void SelectWeapon()
    {
        foreach (WeaponMaster playerWeapon in _playerWeapons.weapons)
        {
            if (weapon.name == playerWeapon.name)
            {
                playerWeapon.LevelUp();
                playerWeapon.level++;
                LevelUpPanel.Instance.ClosePanel();
                return;
            }
        }
        _playerWeapons.AddWeaponToList(weapon.name);
        LevelUpPanel.Instance.ClosePanel();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
