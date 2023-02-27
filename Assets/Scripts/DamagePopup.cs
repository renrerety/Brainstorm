using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class DamagePopup : MonoBehaviour
{
    //Script come from this tutorial : https://www.youtube.com/watch?v=iD1_JczQcFY
    
    [HideInInspector] public DamagePopupPool _damagePopupPool;
    
    public Color textColor;
    public TMP_Text tmp;
    [SerializeField] private Color criticalHitColor;
    [SerializeField] private Color HitColor;
    private float disappearTimer;
    public void Setup(int damageAmount,bool isCritical)
    {
        if (!isCritical)
        {
            tmp.fontSize = 4;
            textColor = HitColor;
        }
        else
        {
            tmp.fontSize = 5;
            textColor = criticalHitColor;
        }
        
        tmp.color = textColor;
        disappearTimer = 1f;
        tmp.text = damageAmount.ToString();
    }
    
    private void Update()
    {
        float moveYSpeed = 1f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 1f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            tmp.color = textColor;
            if (textColor.a < 0)
            {
                textColor.a = 255;
                _damagePopupPool.ReturnObjectToPool(gameObject);
            }
        }
    }
}
