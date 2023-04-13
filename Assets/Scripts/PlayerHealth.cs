using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class PlayerHealth : MonoBehaviour, IPlayerHealth
{
    public static PlayerHealth instance;
    public float hp;
    public float maxHp;
    
    [SerializeField] private float damageCooldown;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject gameOverPanel;

    public AudioSource _audioSource;
    [SerializeField] private AudioClip playerHitClip;
    
    public SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameOverPanel);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    IEnumerator Flicker()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = Color.white;
    }
    
    private float damageTimer;
    private void Update()
    {
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0)
        {
            canTakeDamage = true;
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        gameOverPanel.GetComponent<GameOver>().gameOver = true;
        gameOverPanel.SetActive(true);
        
        BinarySaveFormatter.Serialize();
        
        if (PlayerData.instance.logged)
        {
            StartCoroutine(BinarySaveFormatter.UploadToDb());
        }
    }
    
    
    private bool canTakeDamage;
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            hp -= damage;
            _audioSource.PlayOneShot(playerHitClip);
            StartCoroutine(Flicker());
            canTakeDamage = false;
            damageTimer = damageCooldown;
            UpdateHpBar();
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public bool dot = true;
    public IEnumerator TakeDamageOverTime(float damage)
    {
        if (dot) 
        {
            PlayerHealthProxy.instance.TakeDamage(damage);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(TakeDamageOverTime(damage));
        }
    }

    public void StopDamageOverTime()
    {
        dot = false;
    }

    public void UpdateHpBar()
    {
        hpBar.value = (float)(hp / maxHp);
        fill.color = Color.Lerp(Color.red,Color.green , hp/maxHp);
    }

    public void Init()
    {
        hp = maxHp + (PlayerData.instance.persistentData.upgrades.hpUp) * 5;
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        fill = GameObject.Find("FillHp").GetComponent<Image>();
        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
        UpdateHpBar();
    }
}

public interface IPlayerHealth
{
    public void TakeDamage(float damage);
}
