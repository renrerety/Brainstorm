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
    [SerializeField] private GameObject gameOverPanel;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip playerHitClip;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        _audioSource.PlayOneShot(playerHitClip);
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        UpdateHpBar();
    }

    private void Die()
    {
        Time.timeScale = 0;
        gameOverPanel.GetComponent<GameOver>().gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public IEnumerator TakeDamageOverTime(float damage)
    {
        yield return new WaitForSeconds(0.2f);
        
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
        _audioSource = GetComponent<AudioSource>();
        hp = maxHp;
        UpdateHpBar();
    }
}

public interface IPlayerHealth
{
    public void TakeDamage(float damage);
}
