using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TNRD;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AIMaster : MonoBehaviour
{
    public EnemyDifficulty enemyDifficulty;
    public EnemyType enemyType;

    public IMovement movementStrategy;

    [SerializeField] private float knockbackForce;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] int hp;
    [SerializeField] AudioClip enemyHit;

    [HideInInspector] public EasyEnemyFactory _easyEnemyFactory;
    [HideInInspector] public MediumEnemyFactory _mediumEnemyFactory;
    [HideInInspector] public DamagePopupPool _damagePopupPool;
    [HideInInspector] public XpPool _xpPool;
    [HideInInspector] public EnemySpawner _enemySpawner;

    SpriteRenderer spriteRenderer;
    [HideInInspector] public Transform player;

    [SerializeField] GameObject[] powerUps = new GameObject[5];

    [HideInInspector] public PlayerHealth _playerHealth;
    [HideInInspector] public PlayerHealthProxy _playerHealthProxy;
    [HideInInspector] public PlayerWeapons _playerWeapons;
    [HideInInspector] public KillCounter _killCounter;

    private Rigidbody2D rb;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(enemyHit);
        StartCoroutine(Flicker());

        bool isCritical = Random.Range(0f, 100f) < _playerWeapons.criticalChance ? true : false;
        if (isCritical)
        {
            damage += damage/4;
        }

        _damagePopupPool.TakePooledObject(transform.position,damage,isCritical);
        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Flicker()
    {
        Color temp = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        float speedTemp = speed;
        speed = 0;

        Vector2 direction = (transform.position - player.position).normalized;
        rb.AddForce(direction * knockbackForce,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);

        speed = speedTemp;
        spriteRenderer.color = temp;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        
        RandomDrop();
        DropXp();
        ReturnToPool();
        _killCounter.AddKill();
    }

    private void RandomDrop()
    {
        int rng = Random.Range(0, 101);
        if (rng <= 1)
        {
            GameObject powerUp;
            rng = Random.Range(0, powerUps.Length);
            powerUp = powerUps[rng];
            
            GameObject obj = Instantiate(powerUp);
            obj.GetComponent<PowerUpMaster>()._enemySpawner = _enemySpawner;
            obj.transform.position = gameObject.transform.position;
        }
    }

    private void ReturnToPool()
    {
        switch (enemyDifficulty)
        {
            case EnemyDifficulty.easy:
                _easyEnemyFactory.ReturnEnemyToPool(gameObject);
                break;
            case EnemyDifficulty.medium:
                break;
            case EnemyDifficulty.hard:
                break;
        }
    }

    private void DropXp()
    {
        GameObject xp = _xpPool.TakeXpFromPool();
        xp.transform.position = gameObject.transform.position;
    }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementStrategy = new WalkTowardsPlayer();
    }

    public virtual void Update()
    {
        movementStrategy.Move(gameObject.transform, player,speed);
        FlipTowardsPlayer();
    }

    private void FlipTowardsPlayer()
    {
        float moveX = transform.position.x - player.transform.position.x;

        if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _playerHealth.dot = true;
            _playerHealth.StartCoroutine(_playerHealth.TakeDamageOverTime(damage));
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerHealth.StopDamageOverTime();
        }
    }
}
public enum EnemyType
{
    weak,
    strong
}
public enum EnemyDifficulty
{
    easy,
    hard,
    medium
}
