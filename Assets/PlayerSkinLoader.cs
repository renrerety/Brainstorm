using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    
    
    [SerializeField] private RuntimeAnimatorController superBillController;
    [SerializeField] private RuntimeAnimatorController billController;
    
    // Start is called before the first frame update
    void Start()
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
