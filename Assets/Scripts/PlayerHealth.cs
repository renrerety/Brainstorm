using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealth : MonoBehaviour, IPlayerHealth
{
    [Inject] private PlayerHealthProxy _playerHealthProxy;
    public float hp;
    public float maxHp;

    public bool dot = true;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Image fill;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        UpdateHpBar();
    }

    public IEnumerator TakeDamageOverTime(float damage)
    {
        yield return new WaitForSeconds(0.1f);
        
        if (dot) 
        {
            _playerHealthProxy.TakeDamage(damage);
            StartCoroutine(TakeDamageOverTime(damage));
        }
    }

    public void StopDamageOverTime()
    {
        dot = false;
    }

    void UpdateHpBar()
    {
        hpBar.value = (float)(hp / maxHp);
        Debug.Log((float)(hp / maxHp));
        fill.color = Color.Lerp(Color.red,Color.green , hp/maxHp);
    }

    private void Start()
    {
        hp = maxHp;
        UpdateHpBar();
    }
}

public interface IPlayerHealth
{
    public void TakeDamage(float damage);
}
