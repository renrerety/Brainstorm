using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    public static PlayerSkinLoader instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    
    
    [SerializeField] private RuntimeAnimatorController superBillController;
    [SerializeField] private RuntimeAnimatorController billController;
    
    public void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        switch (PlayerData.instance.currentSkin)
        {
            case SkinList.Bill:
                Debug.Log("Case bill");
                anim.runtimeAnimatorController = billController;
                break;
            case SkinList.SuperBill:
                Debug.Log("Case superBill");
                anim.runtimeAnimatorController = superBillController;
                break;
        }
    }
}
