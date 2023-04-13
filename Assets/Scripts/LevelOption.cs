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

    public void RollWeapon()
    {
        int rng = UnityEngine.Random.Range(0, WeaponRefs.Instance.weaponRefs.Count);
            weapon = WeaponRefs.Instance.weaponRefs[rng];

            image.sprite = weapon.image;

            name.text = Localization.Localization.instance.GetString(weapon.translateKey);
            desc.text = Localization.Localization.instance.GetString(weapon.translateKey+"Desc");
        
        


            if (PlayerWeapons.Instance.FindWeapon(weapon.name))
            {
                desc.text = Localization.Localization.instance.GetString(weapon.translateKey + "LvlDesc");
                Debug.Log(desc.text);
            }
    }
    public void SelectWeapon()
    {
        Debug.Log(weapon.name);
        WeaponMaster findWeapon = PlayerWeapons.Instance.FindWeapon(weapon.name);

        if (findWeapon)
        {
            findWeapon.LevelUp();
            findWeapon.level++;
            LevelUpPanel.Instance.ClosePanel();
            return;
        }
        else
        {
            PlayerWeapons.Instance.AddWeaponToList(this.weapon.name);
            LevelUpPanel.Instance.ClosePanel();
        }
    }
}
