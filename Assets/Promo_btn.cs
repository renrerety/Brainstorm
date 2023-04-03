using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promo_btn : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerData.instance.logged == false)
        {
            gameObject.SetActive(false);
        }
    }
}
