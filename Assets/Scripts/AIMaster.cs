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
using Random = UnityEngine.Random;

public class AIMaster : MonoBehaviour
{

    public EnemyDifficulty enemyDifficulty;
    public EnemyType enemyType;

    public IMovement movementStrategy;

    [SerializeField] private float knockbackForce;
    [SerializeField] float damage;
    [SerializeField] float speed;
    public int hp;
    public int maxHp;
    [SerializeField] AudioClip enemyHit;
    
    SpriteRenderer spriteRenderer;
    [HideInInspector] public Transform player;

    [SerializeField] GameObject[] powerUps = new GameObject[5];
    

    private Rigidbody2D rb;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(enemyHit);
        StartCoroutine(Flicker());

        bool isCritical = Random.Range(0f, 100f) < PlayerWeapons.Instance.criticalChance ? true : false;
        if (isCritical)
        {
            damage += damage/4;
        }

        DamagePopupPool.instance.TakePooledObject(transform.position,damage,isCritical);
        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private bool isFlickering;
    IEnumerator Flicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            Color temp = spriteRenderer.color;
            spriteRenderer.color = Color.red;

            float speedTemp = speed;
            speed = 0;

            Vector2 direction = (transform.position - player.position).normalized;
            rb.AddForce(direction * knockbackForce,ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);

            speed = speedTemp;
            spriteRenderer.color = temp;
            rb.velocity = Vector2.zero;

            isFlickering = false;
        }
        
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        
        RandomDrop();
        DropXp();
        IncrementPlayerStats();
        ReturnToPool();
        KillCounter.instance.AddKill();
    }

    private void RandomDrop()
    {
        int rng = Random.Range(0, 1001);
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
                EasyEnemyFactory.instance.ReturnEnemyToPool(gameObject);
                break;
            case EnemyDifficulty.medium:
                MediumEnemyFactory.instance.ReturnEnemyToPool(gameObject);
                break;
            case EnemyDifficulty.hard:
                HardEnemyFactory.instance.ReturnEnemyToPool(gameObject);
                break;
        }
    }

    private void DropXp()
    {
        GameObject xp;
        switch (enemyDifficulty)
        {
            case EnemyDifficulty.easy:
               xp = XpPool.instance.TakeBlueXpFromPool();
                break;
            case EnemyDifficulty.medium:
                xp = XpPool.instance.TakeYellowXpFromPool();
                break;
            case EnemyDifficulty.hard:
                xp = XpPool.instance.TakeRedXpFromPool();
                break;
            default:
                xp = XpPool.instance.TakeRedXpFromPool();
                break;
        }
        xp.transform.position = gameObject.transform.position;
    }

    private void IncrementPlayerStats()
    {
        PlayerData.instance.persistentData.gold++;
        PlayerData.instance.persistentData.kills++;
    }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementStrategy = ScriptableObject.CreateInstance<WalkTowardsPlayer>();
        hp = maxHp;
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
            PlayerHealth.instance.dot = true;
            PlayerHealth.instance.StartCoroutine(PlayerHealth.instance.TakeDamageOverTime(damage));
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.StopDamageOverTime();
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
    medium,
    hard
}
