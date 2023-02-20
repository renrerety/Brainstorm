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

    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] int hp;
    [SerializeField] AudioClip enemyHit;

    public EasyEnemyFactory _easyEnemyFactory;
    public MediumEnemyFactory _mediumEnemyFactory;
    public XpPool _xpPool;

    SpriteRenderer spriteRenderer;
    public Transform player;

    [SerializeField] GameObject[] powerUps = new GameObject[5];

    public PlayerHealth _playerHealth;
    public PlayerHealthProxy _playerHealthProxy;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(enemyHit);
        StartCoroutine(Flicker());
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _playerHealth.dot = true;
            _playerHealth.StartCoroutine(_playerHealth.TakeDamageOverTime(damage));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
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
